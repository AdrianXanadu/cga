using CGA_Server.Models;
using System;
using System.Collections.Generic;
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

namespace CGA_Client
{
    /// <summary>
    /// Interaktionslogik für AdministratorView.xaml
    /// </summary>
    public partial class AdministratorView : Window
    {
        Administrator Administrator { get; set; }  
        public AdministratorView(Administrator admin)
        {
            InitializeComponent();

            Administrator = admin;

            textBlock_name.Text = admin.Name;
        }

        private void button_logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mv = new MainWindow();
            mv.Show();
            this.Close();
        }
    }
}
