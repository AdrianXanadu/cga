using CGA_Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
        public Country SelectedCountry { get; set; } = new Country();
        private int DefaultId = 0;
        public ObservableCollection<Country> Countries { get; set; } = new ObservableCollection<Country>();
        public ModifyCountryWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void listBox_countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Country selection = listBox_countries.SelectedItem as Country;

            SelectedCountry.Size = selection.Size;
            SelectedCountry.Name = selection.Name;
            SelectedCountry.NameNative = selection.NameNative;
            SelectedCountry.Flag = selection.Flag;
            SelectedCountry.Iso31661Alpha3Code = selection.Iso31661Alpha3Code;
            SelectedCountry.Population = selection.Population;
            SelectedCountry.Cid = selection.Cid;
            SelectedCountry.Iid = selection.Iid;
            SelectedCountry.Location = selection.Location;

           
        }

        private async void countryWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await GetCountriesAsync();
            DefaultId = await GetHighestCountryIdAsync() + 1;
            textBox_id.Text = Convert.ToString(DefaultId);
        }

        private async Task<int> GetHighestCountryIdAsync()
        {
            var result = await MainWindow.HTTP_CLIENT.GetAsync("/api/countries/max");

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("ERROR");
            }

            return Convert.ToInt32(await result.Content.ReadAsStringAsync());
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

        private async void button_save_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(textBox_id.Text) == DefaultId)
            {
                // add new country
                Country country = new Country();
                country.Cid = Convert.ToInt32(textBox_id.Text);
                country.Name = textBox_name.Text;
                country.NameNative = textBox_name_native.Text;
                country.Population = Convert.ToInt32(textBox_population.Text);
                country.Size = Convert.ToInt32(textBox_size.Text);
                country.Flag = Convert.ToString(image_flag.Source);
                country.Iso31661Alpha3Code = textBox_iso3166_code.Text;

                await AddCountryAsync(country);
            } else
            {
                // modify country
                Country country = new Country();
                country.Cid = Convert.ToInt32(textBox_id.Text);
                country.Name = textBox_name.Text;
                country.NameNative = textBox_name_native.Text;
                country.Population = Convert.ToInt32(textBox_population.Text);
                country.Size = Convert.ToInt32(textBox_size.Text);
                country.Flag = Convert.ToString(image_flag.Source);
                country.Iso31661Alpha3Code = textBox_iso3166_code.Text;
                country.Iid = SelectedCountry.Iid;
                country.Location = SelectedCountry.Location;

                await ModifyCountryAsync(country);
            }
        }

        private async Task ModifyCountryAsync(Country country)
        {
            StringContent countryStringContent = new StringContent(JsonSerializer.Serialize(country, MainWindow.JSON_SERIALIZER_OPTIONS), Encoding.UTF8, "application/json");

            var result = await MainWindow.HTTP_CLIENT.PatchAsync($"/api/countries/{country.Cid}", countryStringContent);

            this.Close();
        }

        private async Task<Country> AddCountryAsync(Country country)
        {
            StringContent countryStringContent = new StringContent(JsonSerializer.Serialize<Country>(country, MainWindow.JSON_SERIALIZER_OPTIONS), Encoding.UTF8, "application/json");

            var result = await MainWindow.HTTP_CLIENT.PostAsync("/api/countries", countryStringContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("ERROR");
            }

            var resultString = await result.Content.ReadAsStringAsync();

            this.Close();

            return JsonSerializer.Deserialize<Country>(resultString, MainWindow.JSON_SERIALIZER_OPTIONS);
        }
    }
}
