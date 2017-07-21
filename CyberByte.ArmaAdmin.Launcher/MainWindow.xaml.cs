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
using System.IO;
using CyberByte.ArmaAdmin.Launcher.Services;
using CyberByte.ArmaAdmin.Launcher.Models;

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
            LaunchProcess(Properties.Settings.Default.teamspeak);
        }

        private void Forums_Click(object sender, RoutedEventArgs e)
        {
            LaunchProcess(Properties.Settings.Default.website);
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Download download = new Download();
                download.Setup();
            } catch (Exception exception)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Unable to get latest mods. Press OK to contine without checking.",
                    "Unable to update",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Information
                );
                if (result != MessageBoxResult.OK)
                {
                    return;
                }
            }
            Arma3Service arma3 = new Arma3Service();
            arma3.Launch();
        }

        private Process LaunchProcess(string path)
        {
            Process process = new Process();
            process.StartInfo.FileName = path;
            process.Start();
            return process;
        }
    }
}
