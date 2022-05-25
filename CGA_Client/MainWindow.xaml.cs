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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CGA_Client.Views;

namespace CGA_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static HttpClient HTTP_CLIENT = new HttpClient()
        {
            BaseAddress = new Uri(@"https://localhost:7219")
        };

        public static JsonSerializerOptions JSON_SERIALIZER_OPTIONS = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            rw.Show();
            this.Close();
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
        }
    }
}
