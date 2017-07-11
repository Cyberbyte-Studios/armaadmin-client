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
            if (!File.Exists(Properties.Settings.Default.arma_exe))
            {
                InvalidArmaLocation();
                return;
            }
            LaunchProcess($"{Properties.Settings.Default.arma_exe} {Properties.Settings.Default.server_options} {Properties.Settings.Default.launch_options}");
        }

        private void InvalidArmaLocation()
        {
            MessageBoxResult result = MessageBox.Show(
                "Arma 3 location invalid. Press OK to edit.",
                "Invalid Arma 3 Location",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning
            );
            if (result == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
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
