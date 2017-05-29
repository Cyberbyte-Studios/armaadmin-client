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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Collections.ObjectModel;
using CyberByte.ArmaAdmin.Launcher.Models;
using CyberByte.ArmaAdmin.Launcher.Services;

namespace CyberByte.ArmaAdmin.Launcher
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        private LoginService loginService;
        private User user = new User();

        public LoginWindow()
        {
            InitializeComponent();
            loginService = new LoginService();
            this.LoginForm.DataContext = user;
        }


        private void Login(object sender, RoutedEventArgs e)
        {
            user.Username = "";
            user.Password = "";

            loginService.Login(user);
        }
    }
}
