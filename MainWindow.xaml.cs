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

        private void SetActiveNav(string pageName)
        {
            HomeButton.Style = (Style)FindResource("NavButtonStyle");
            UpdatesButton.Style = (Style)FindResource("NavButtonStyle");
            FeaturesButton.Style = (Style)FindResource("NavButtonStyle");
            SettingsButton.Style = (Style)FindResource("NavButtonStyle");

            switch (pageName)
            {
                case "Home":
                    HomeButton.Style = (Style)FindResource("ActiveNavButtonStyle");
                    HomePanel.Visibility = Visibility.Visible;
                    PlaceholderPanel.Visibility = Visibility.Collapsed;
                    StatusText.Text = "• Ready to launch";
                    break;

                case "Updates":
                    UpdatesButton.Style = (Style)FindResource("ActiveNavButtonStyle");
                    HomePanel.Visibility = Visibility.Collapsed;
                    PlaceholderPanel.Visibility = Visibility.Visible;
                    MainTitle.Text = "Updates";
                    StatusText.Text = "• No update check implemented yet";
                    break;

                case "Features":
                    FeaturesButton.Style = (Style)FindResource("ActiveNavButtonStyle");
                    HomePanel.Visibility = Visibility.Collapsed;
                    PlaceholderPanel.Visibility = Visibility.Visible;
                    MainTitle.Text = "Features";
                    StatusText.Text = "• Feature overview";
                    break;

                case "Settings":
                    SettingsButton.Style = (Style)FindResource("ActiveNavButtonStyle");
                    HomePanel.Visibility = Visibility.Collapsed;
                    PlaceholderPanel.Visibility = Visibility.Visible;
                    MainTitle.Text = "Settings";
                    StatusText.Text = "• Settings panel";
                    break;
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveNav("Home");
        }

        private void UpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveNav("Updates");
        }

        private void FeaturesButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveNav("Features");
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveNav("Settings");
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string gamePath = Path.Combine(baseDirectory, "SupremeGDPS.exe");

            if (!File.Exists(gamePath))
            {
                StatusText.Text = "• Launch failed";
                MessageBox.Show(
                    "SupremeGDPS.exe was not found in the launcher directory.",
                    "Launch Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = gamePath,
                    WorkingDirectory = baseDirectory,
                    UseShellExecute = true
                });

                StatusText.Text = "• Game launched successfully";
            }
            catch (Exception ex)
            {
                StatusText.Text = "• Launch failed";
                MessageBox.Show(
                    $"The game could not be launched.\n\n{ex.Message}",
                    "Launch Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private void OpenGameDirectory_Click(object sender, RoutedEventArgs e)
        {
            string gameDirectory = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = gameDirectory,
                    UseShellExecute = true
                });

                StatusText.Text = "• Opened game directory";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"The game directory could not be opened.\n\n{ex.Message}",
                    "Directory Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private void OpenAppDataFolder_Click(object sender, RoutedEventArgs e)
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string supremeAppDataFolder = Path.Combine(localAppData, "SupremeGDPS");

            try
            {
                Directory.CreateDirectory(supremeAppDataFolder);

                Process.Start(new ProcessStartInfo
                {
                    FileName = supremeAppDataFolder,
                    UseShellExecute = true
                });

                StatusText.Text = "• Opened AppData folder";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"The AppData folder could not be opened.\n\n{ex.Message}",
                    "Folder Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}