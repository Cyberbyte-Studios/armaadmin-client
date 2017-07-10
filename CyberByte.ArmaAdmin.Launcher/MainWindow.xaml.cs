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
using System.Diagnostics;
using MahApps.Metro.Controls;

namespace CyberByte.ArmaAdmin.Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //string uri = AppDomain.CurrentDomain.BaseDirectory + "news.html";
            //this.browser.Navigate(new Uri(uri, UriKind.Absolute));
        }

        private void Teamspeak_Click(object sender, RoutedEventArgs e)
        {
            Process teamspeak = new Process();
            teamspeak.StartInfo.FileName = "ts3server://ts3.criticalgaming.org?port=9987"; // populate from settings
            teamspeak.Start();
        }

        private void Forums_Click(object sender, RoutedEventArgs e)
        {
            Process forums = new Process();
            forums.StartInfo.FileName = "https://forums.criticalgaming.org";
            forums.Start();
        }

        private void Discord_Click(object sender, RoutedEventArgs e)
        {
            Process discord = new Process();
            discord.StartInfo.FileName = "https://discord.gg/U4akubm"; // populate from settingshttps://discord.gg/U4akubm
            discord.Start();
        }
    }
}
