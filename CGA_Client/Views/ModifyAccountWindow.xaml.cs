using CGA_Client.Utils;
using CGA_Server.Models;
using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für ModifyAccountWindow.xaml
    /// </summary>
    public partial class ModifyAccountWindow : Window
    {
        Player Player { get; set; }
        public ModifyAccountWindow(Player player)
        {
            InitializeComponent();

            Player = player;

            textBox_username.Text = player.Name;
        }

        private async void button_save_Click(object sender, RoutedEventArgs e)
        {
            Player.Name = textBox_username.Text;
            Player.Password = Cryptology.SHA256Hash(textBox_password.Text);

            StringContent playerStringContent = new StringContent(JsonSerializer.Serialize(Player, MainWindow.JSON_SERIALIZER_OPTIONS), Encoding.UTF8, "application/json");

            var result =  await MainWindow.HTTP_CLIENT.PatchAsync($"/api/players/{Player.Id}", playerStringContent);

            this.Close();
        }
    }
}
