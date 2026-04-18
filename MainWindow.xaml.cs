using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SupremeGDPSLauncher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainTitle.Text = "Welcome to Supreme GDPS";
        }

        private void UpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            MainTitle.Text = "Updates";
        }

        private void FeaturesButton_Click(object sender, RoutedEventArgs e)
        {
            MainTitle.Text = "Features";
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainTitle.Text = "Settings";
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string gamePath = Path.Combine(baseDir, "SupremeGDPS.exe");

            if (!File.Exists(gamePath))
            {
                MessageBox.Show("SupremeGDPS.exe was not found",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = gamePath,
                WorkingDirectory = baseDir,
                UseShellExecute = true
            });
        }

        private void OpenGameDirectory_Click(object sender, RoutedEventArgs e)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            Process.Start(new ProcessStartInfo
            {
                FileName = baseDir,
                UseShellExecute = true
            });
        }

        private void OpenAppDataFolder_Click(object sender, RoutedEventArgs e)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string targetFolder = Path.Combine(appData, "SupremeGDPS");

            Directory.CreateDirectory(targetFolder);

            Process.Start(new ProcessStartInfo
            {
                FileName = targetFolder,
                UseShellExecute = true
            });
        }
    }
}