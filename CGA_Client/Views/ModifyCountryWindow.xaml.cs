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

namespace CGA_Client.Views
{
    /// <summary>
    /// Interaktionslogik für ModifyCountryWindow.xaml
    /// </summary>
    public partial class ModifyCountryWindow : Window
    {
        public Country SelectedCountry { get; set; }
        public ObservableCollection<Country> Countries { get; set; } = new ObservableCollection<Country>();
        public ModifyCountryWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void listBox_countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedCountry = listBox_countries.SelectedItem as Country;
        }

        private async void countryWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await GetCountriesAsync();
        }

        private async Task GetCountriesAsync()
        {
            var result = await MainWindow.HTTP_CLIENT.GetAsync($"/api/countries");

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show("Couldn't load countries!");
            }

            var countries = JsonSerializer.Deserialize<List<Country>>(await result.Content.ReadAsStringAsync(), MainWindow.JSON_SERIALIZER_OPTIONS);

            foreach (Country c in countries)
            {
                Countries.Add(c);
            }

            return;
        }

        private async Task DeleteCountryAsync(int id)
        {
            var result = await MainWindow.HTTP_CLIENT.DeleteAsync($"/api/countries/{id}");

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show("Couldn't delete country, first delete locations referencing this country.");
            }

            MessageBox.Show("Deleted!");

            return;
        }

        private async void button_delete_Click(object sender, RoutedEventArgs e)
        {
            await DeleteCountryAsync(SelectedCountry.Cid);
        }
    }
}
