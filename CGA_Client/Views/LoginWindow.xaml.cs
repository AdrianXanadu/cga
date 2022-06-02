using CGA_Client.Utils;
using CGA_Server.Models;
using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void button_login_Click(object sender, RoutedEventArgs e)
        {
            // Login Data Admin: User: Verwalter - Password: 1234
            // Login Data Sample User: User: Eno - Password: 12

            if (checkBox_admin.IsChecked == false)
            {
                var result = await MainWindow.HTTP_CLIENT.GetAsync($"/api/players/name/{textBox_username.Text}");

                if (!result.IsSuccessStatusCode)
                {
                    MessageBox.Show("User not found!");
                }

                Player player = JsonSerializer.Deserialize<Player>(await result.Content.ReadAsStringAsync(), MainWindow.JSON_SERIALIZER_OPTIONS);

                if (player.Password == Cryptology.SHA256Hash(textBox_password.Text))
                {
                    PlayerView pv = new PlayerView(player);
                    pv.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Password!");
                }

            } else
            {
                var result = await MainWindow.HTTP_CLIENT.GetAsync($"/api/administrators/name/{textBox_username.Text}");

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("User not found!");
                }

                Administrator admin = JsonSerializer.Deserialize<Administrator>(await result.Content.ReadAsStringAsync(), MainWindow.JSON_SERIALIZER_OPTIONS);

                if (admin.Password == Cryptology.SHA256Hash(textBox_password.Text))
                {
                    AdministratorView av = new AdministratorView(admin);
                    av.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Password!");
                }
            }
            

            
           
        }
    }
}
