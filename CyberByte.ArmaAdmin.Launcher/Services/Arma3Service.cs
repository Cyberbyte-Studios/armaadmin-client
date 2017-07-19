using System;
using Microsoft.Win32;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace CyberByte.ArmaAdmin.Launcher.Services
{
    class Arma3Service
    {
        public void Launch()
        {
            if (!ValidLocation())
            {
                InvalidLocation();
                if (BrowseLocation() != true) return;
                UpdatedLocation(launch: true);
            }
            RunArma();
        }

        public bool BrowseLocation()
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                FileName = "arma3",
                DefaultExt = ".exe",
                Filter = "Executable Files (.exe)|*.exe"
            };

            Nullable<bool> result = dlg.ShowDialog();

            if (result != true)
            {
                return false;
            }

            if (ValidLocation(dlg.FileName))
            {
                SaveLocation(dlg.FileName);
                return true;
            }
            return false;
        }

        private void InvalidLocation()
        {
            MessageBoxResult result = MessageBox.Show(
                "Arma 3 location invalid.",
                "Invalid Arma 3 Location",
                MessageBoxButton.OK,
                MessageBoxImage.Warning
            );
        }

        private bool UpdatedLocation(bool launch = false)
        {
            string message = "Arma 3 location has been updated.";
            MessageBoxButton button = MessageBoxButton.OK;
            if (launch)
            {
                message += " Press OK to continue Arma 3 Launch.";
                button = MessageBoxButton.OKCancel;
            }
            MessageBoxResult result = MessageBox.Show(
                message,
                "Updated Arma 3 Location",
                button,
                MessageBoxImage.Information
            );
            if (result == MessageBoxResult.OK && launch == true)
            {
                return true;
            }
            return false;
        }

        private bool ValidLocation(string location=null)
        {
            if (location == null)
            {
                location = Properties.Settings.Default.arma_exe;
            }
            return File.Exists(location);
        }

        private void SaveLocation(string location)
        {
            Properties.Settings.Default.arma_exe = location;
            Properties.Settings.Default.Save();
        }

        public void RunArma()
        {
            LaunchProcess(Properties.Settings.Default.arma_exe, $"{Properties.Settings.Default.server_options} {Properties.Settings.Default.launch_options}");
        }

        private Process LaunchProcess(string path, string arguments="")
        {
            Process process = new Process();
            process.StartInfo.FileName = path;
            process.StartInfo.Arguments = arguments;
            process.Start();
            return process;
        }
    }
}
