using CGA_Client.Views;
using CGA_Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CGA_Client
{
    /// <summary>
    /// Interaktionslogik für AdministratorView.xaml
    /// </summary>
    public partial class AdministratorView : Window
    {
        public Administrator Administrator { get; set; }
        public ObservableCollection<Score> Scores { get; set; } = new ObservableCollection<Score>();
        public AdministratorView(Administrator admin)
        {
            InitializeComponent();
            this.DataContext = this;
            Administrator = admin;
            
        }

        private void button_logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mv = new MainWindow();
            mv.Show();
            this.Close();
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

        private async void window_administrator_Loaded(object sender, RoutedEventArgs e)
        {
            await GetScoresAsync();
        }
    }
}
