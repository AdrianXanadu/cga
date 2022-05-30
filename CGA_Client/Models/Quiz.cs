using CGA_Client.Views;
using CGA_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace CGA_Client.Models
{
    internal class Quiz
    {
        private static PlayerView PlayerWindow;
        private static int Rounds = 3;

        public Quiz(PlayerView pv)
        {
            PlayerWindow = pv;
        }

        public async Task Start()
        {
            var qp = await GetRandomQuestionPreset();

            // DEBUG

            if (qp.Name == "Population")
            {
                var countries = await GetRandomCountries();
                var correctCountry = countries[0];

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
