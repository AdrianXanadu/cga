using CGA_Client.Models;
using CGA_Server.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CGA_Client.Views
{
    /// <summary>
    /// Interaktionslogik für PlayerView.xaml
    /// </summary>
    public partial class PlayerView : Window
    {
        enum ColourScheme
        {
            Light,
            Dark
        }
        public Player Player { get; set; }
        Quiz Quiz { get; set; }

        public ObservableCollection<Score> Scores { get; set; } = new ObservableCollection<Score>();

        public PlayerView(Player player)
        {
            InitializeComponent();
            this.DataContext = this;
            Player = player;
        }
        private async Task GetScoresAsync()
        {
            var result = await MainWindow.HTTP_CLIENT.GetAsync($"/api/scores");

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show("Couldn't load scores!");
            }

            var scores = JsonSerializer.Deserialize<List<Score>>(await result.Content.ReadAsStringAsync(), MainWindow.JSON_SERIALIZER_OPTIONS);

            foreach (Score s in scores)
            {
                Scores.Add(s);
            }

            return;
        }

        public void LoadSettings()
        {

            var fileName = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\CGA\Settings.csv";

            if (!File.Exists(fileName))
            {
                new FileInfo(fileName).Directory.Create();

                using (var writer = new StreamWriter(fileName))
                {
                    writer.WriteLine($"{ColourScheme.Light}");
                    writer.WriteLine($"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}");
                }

                button_export_scores_path.Content = $@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}";
                comboBox_colour_scheme.ItemsSource = new [] { ColourScheme.Light, ColourScheme.Dark };
            } else
            {
                var lines = File.ReadAllLines(fileName);

                button_export_scores_path.Content = lines[1];
                comboBox_colour_scheme.ItemsSource = new[] { ColourScheme.Light, ColourScheme.Dark };
                comboBox_colour_scheme.SelectedIndex = (int) Enum.Parse(typeof(ColourScheme), lines[0]);

                if ((ColourScheme) comboBox_colour_scheme.SelectedIndex == ColourScheme.Dark)
                {
                    SwitchToDarkMode();
                }
            }
        }

        private void button_logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mv = new MainWindow();
            mv.Show();
            this.Close();
        }

        private void button_change_account_information_Click(object sender, RoutedEventArgs e)
        {
            ModifyAccountWindow maw = new ModifyAccountWindow(Player);
            maw.Show();
        }

        private async void button_export_scores_path_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                button_export_scores_path.Content = @$"{dialog.SelectedPath}";
            }


        }

        private void button_save_settings_Click(object sender, RoutedEventArgs e)
        {
            var fileName = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\CGA\Settings.csv";

            using (var writer = new StreamWriter(fileName, false))
            {
                writer.WriteLine(comboBox_colour_scheme.SelectedItem);
                writer.WriteLine(button_export_scores_path.Content);
            }

            MessageBox.Show("Successfully saved!");
        }

        private void comboBox_colour_scheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((ColourScheme) comboBox_colour_scheme.SelectedIndex == ColourScheme.Dark)
            {
                SwitchToDarkMode();
            } else
            {
                SwitchToLightMode();
            }
        }

        private void SwitchToDarkMode()
        {
            //MessageBox.Show("Switched to Dark Mode");
        }

        private void SwitchToLightMode()
        {
            //MessageBox.Show("Switched to Light Mode");
        }

        private void button_export_stats_Click(object sender, RoutedEventArgs e)
        {
            using (var writer = new StreamWriter((string) @$"{button_export_scores_path.Content}\exports.txt"))
            {
                foreach (Score s in listBox_scores.Items)
                {
                    writer.WriteLine(s.ToString());
                }
            }

            MessageBox.Show("Exported!");
        }

        private async void window_player_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
            await GetScoresAsync();

            Quiz = new Quiz(this);
            await Quiz.GenerateQuiz();
        }

        private async void button_answer_Click(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);

            if (!(button.Content == null || Convert.ToString(button.Content) == ""))
            {
                await Quiz.ButtonAnswer(Convert.ToString(button.Content));
            }
        }
    }
}
