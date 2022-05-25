using CGA_Client.Utils;
using CGA_Server.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace CGA_Client.Views
{
    /// <summary>
    /// Interaktionslogik für RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async Task<Player> CreatePlayerAsync(Player player)
        {
            StringContent playerStringContent = new StringContent(JsonSerializer.Serialize<Player>(player, MainWindow.JSON_SERIALIZER_OPTIONS), Encoding.UTF8, "application/json");

            var result = await MainWindow.HTTP_CLIENT.PostAsync("/api/players", playerStringContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("ERROR");
            }

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Player>(resultString, MainWindow.JSON_SERIALIZER_OPTIONS);
        }

        private async void button_register_Click(object sender, RoutedEventArgs e)
        {
            var result = await MainWindow.HTTP_CLIENT.GetAsync("/api/players/max");

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("ERROR");
            }

            Player p = new Player(int.Parse(await result.Content.ReadAsStringAsync()) + 1, textBox_username.Text, Cryptology.SHA256Hash(textBox_password.Text), DateTime.UtcNow);

            PlayerView pv = new PlayerView(await CreatePlayerAsync(p));
            pv.Show();
            this.Close();
        }
    }
}
