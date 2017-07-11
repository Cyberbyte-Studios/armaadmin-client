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
            this.browser.Navigate(new Uri(Properties.Settings.Default.launcher_url, UriKind.Absolute));
        }

        private void Teamspeak_Click(object sender, RoutedEventArgs e)
        {
            Process teamspeak = new Process();
            teamspeak.StartInfo.FileName = Properties.Settings.Default.teamspeak;
            teamspeak.Start();
        }

        private void Forums_Click(object sender, RoutedEventArgs e)
        {
            Process forums = new Process();
            forums.StartInfo.FileName = Properties.Settings.Default.website;
            forums.Start();
        }
    }
}
