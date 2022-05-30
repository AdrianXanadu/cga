﻿using CGA_Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Windows.Storage.Pickers;

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
        Player Player { get; set; }
        

        public PlayerView(Player player)
        {
            InitializeComponent();
            Player = player;

            LoadSettings();

            textBlock_name.Text = player.Name;
            listBox_scores.ItemsSource = player.Score; 
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

                button_export_scores_path.Content = $@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\export.csv";
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
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    button_export_scores_path.Content = dialog.SelectedPath;
                }
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
    }
}
