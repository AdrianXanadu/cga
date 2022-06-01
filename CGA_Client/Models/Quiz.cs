using CGA_Client.Views;
using CGA_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace CGA_Client.Models
{
    internal class Quiz
    {
        private PlayerView PlayerWindow;
        private static int Rounds = 3;
        private string CorrectAnswer;
        public int Score { get; set; }

        public Quiz(PlayerView pv)
        {
            PlayerWindow = pv;
        }

        private async Task<Score> CreateScoreAsync()
        {
            var result = await MainWindow.HTTP_CLIENT.GetAsync("/api/scores/max");

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("ERROR");
            }

            Score score = new Score()
            {
                Date = DateTime.UtcNow,
                Pid = PlayerWindow.Player.Id,
                PidNavigation = PlayerWindow.Player,
                Score1 = Score,
                Sid = int.Parse(await result.Content.ReadAsStringAsync()) + 1
            };

            StringContent scoreStringContent = new StringContent(JsonSerializer.Serialize<Score>(score, MainWindow.JSON_SERIALIZER_OPTIONS), Encoding.UTF8, "application/json");

            result = await MainWindow.HTTP_CLIENT.PostAsync("/api/scores", scoreStringContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("ERROR");
            }

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Score>(resultString, MainWindow.JSON_SERIALIZER_OPTIONS);
        }

        private async Task StopAsync()
        {
            MessageBox.Show($"Game Over! Score: {Score}");
            PlayerWindow.Scores.Add(await CreateScoreAsync());
            Score = 0;
            await GenerateQuiz();
        }

        public async Task ButtonAnswer(string answer)
        {
            if (answer == CorrectAnswer)
            {
                Score++;
                await GenerateQuiz();
            } else
            {
                await StopAsync();
            }
        }

        public async Task GenerateQuiz()
        {
            var qp = await GetRandomQuestionPreset();

            // DEBUG

            if (qp.Name == "Population")
            {
                var countries = await GetRandomCountries();
                var correctCountry = countries[0];

                CorrectAnswer = Convert.ToString(correctCountry.Population);

                PlayerWindow.textBlock_text.Text = qp.Preset.Replace("${}", correctCountry.Name);

                var rand = new Random();
                var countriesShuffled = countries.OrderBy(x => rand.Next()).ToList();

                PlayerWindow.button_answer_1.Content = countriesShuffled[0].Population;
                PlayerWindow.button_answer_2.Content = countriesShuffled[1].Population;
                PlayerWindow.button_answer_3.Content = countriesShuffled[2].Population;
                PlayerWindow.button_answer_4.Content = countriesShuffled[3].Population;

            } else if (qp.Name == "Area")
            {
                var countries = await GetRandomCountries();
                var correctCountry = countries[0];

                CorrectAnswer = Convert.ToString(correctCountry.Size);

                PlayerWindow.textBlock_text.Text = qp.Preset.Replace("${}", correctCountry.Name);

                var rand = new Random();
                var countriesShuffled = countries.OrderBy(x => rand.Next()).ToList();

                PlayerWindow.button_answer_1.Content = countriesShuffled[0].Size;
                PlayerWindow.button_answer_2.Content = countriesShuffled[1].Size;
                PlayerWindow.button_answer_3.Content = countriesShuffled[2].Size;
                PlayerWindow.button_answer_4.Content = countriesShuffled[3].Size;
            } else if (qp.Name == "Native-Name")
            {
                var countries = await GetRandomCountries();
                var correctCountry = countries[0];

                CorrectAnswer = Convert.ToString(correctCountry.NameNative);

                PlayerWindow.textBlock_text.Text = qp.Preset.Replace("${}", correctCountry.Name);

                var rand = new Random();
                var countriesShuffled = countries.OrderBy(x => rand.Next()).ToList();

                PlayerWindow.button_answer_1.Content = countriesShuffled[0].NameNative;
                PlayerWindow.button_answer_2.Content = countriesShuffled[1].NameNative;
                PlayerWindow.button_answer_3.Content = countriesShuffled[2].NameNative;
                PlayerWindow.button_answer_4.Content = countriesShuffled[3].NameNative;
            } else if (qp.Name == "Name")
            {
                var countries = await GetRandomCountries();
                var correctCountry = countries[0];

                CorrectAnswer = Convert.ToString(correctCountry.Name);

                PlayerWindow.textBlock_text.Text = qp.Preset.Replace("${}", correctCountry.NameNative);

                var rand = new Random();
                var countriesShuffled = countries.OrderBy(x => rand.Next()).ToList();

                PlayerWindow.button_answer_1.Content = countriesShuffled[0].Name;
                PlayerWindow.button_answer_2.Content = countriesShuffled[1].Name;
                PlayerWindow.button_answer_3.Content = countriesShuffled[2].Name;
                PlayerWindow.button_answer_4.Content = countriesShuffled[3].Name;
            } else if (qp.Name == "Image")
            {
                MessageBox.Show("Not implemented!");
            } else
            {
                throw new NotImplementedException();
            }
        }

        private async Task<QuestionPreset> GetRandomQuestionPreset()
        {
            var result = await MainWindow.HTTP_CLIENT.GetAsync($"/api/questionpresets");

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show("Couldn't load presets!");
            }

            var presets = JsonSerializer.Deserialize<List<QuestionPreset>>(await result.Content.ReadAsStringAsync(), MainWindow.JSON_SERIALIZER_OPTIONS);

            Random r = new Random();
            return presets[r.Next(presets.Count)];
        }

        private async Task<List<Country>> GetRandomCountries()
        {
            var result = await MainWindow.HTTP_CLIENT.GetAsync($"/api/countries");

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show("Couldn't load countries!");
            }

            var countries = JsonSerializer.Deserialize<List<Country>>(await result.Content.ReadAsStringAsync(), MainWindow.JSON_SERIALIZER_OPTIONS);

            Random r = new Random();

            var countrySelection = new List<Country>();

            for (int i = 0; i < 4; i++)
            {
                countrySelection.Add(countries[r.Next(countries.Count)]);
            }

            return countrySelection;
        }
    }
}
