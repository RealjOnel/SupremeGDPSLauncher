using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace SupremeGDPSLauncher
{
    public partial class MainWindow : Window
    {
        private readonly string _baseDirectory;
        private readonly string _gameExePath;
        private readonly string _resourcesFolderPath;

        public MainWindow()
        {
            InitializeComponent();

            _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _gameExePath = Path.Combine(_baseDirectory, "SupremeGDPS.exe");
            _resourcesFolderPath = Path.Combine(_baseDirectory, "Resources");

            SetActiveNav("Home");
            ValidateInstallation();
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
                    break;

                case "Updates":
                    UpdatesButton.Style = (Style)FindResource("ActiveNavButtonStyle");
                    HomePanel.Visibility = Visibility.Collapsed;
                    PlaceholderPanel.Visibility = Visibility.Visible;
                    MainTitle.Text = "Updates";
                    SetNeutralStatus("• No update check implemented yet");
                    break;

                case "Features":
                    FeaturesButton.Style = (Style)FindResource("ActiveNavButtonStyle");
                    HomePanel.Visibility = Visibility.Collapsed;
                    PlaceholderPanel.Visibility = Visibility.Visible;
                    MainTitle.Text = "Features";
                    SetNeutralStatus("• Feature overview");
                    break;

                case "Settings":
                    SettingsButton.Style = (Style)FindResource("ActiveNavButtonStyle");
                    HomePanel.Visibility = Visibility.Collapsed;
                    PlaceholderPanel.Visibility = Visibility.Visible;
                    MainTitle.Text = "Settings";
                    SetNeutralStatus("• Settings panel");
                    break;
            }
        }

        private void ValidateInstallation()
        {
            bool hasGameExe = File.Exists(_gameExePath);
            bool hasResources = Directory.Exists(_resourcesFolderPath);

            if (hasGameExe && hasResources)
            {
                SetSuccessStatus("• Ready to launch");
                return;
            }

            if (!hasGameExe && !hasResources)
            {
                SetErrorStatus("• SupremeGDPS.exe and Resources are missing");
                return;
            }

            if (!hasGameExe)
            {
                SetErrorStatus("• SupremeGDPS.exe is missing");
                return;
            }

            if (!hasResources)
            {
                SetErrorStatus("• Resources folder is missing");
            }
        }

        private void SetNeutralStatus(string message)
        {
            StatusText.Text = message;
            StatusText.Foreground = new SolidColorBrush(Color.FromRgb(139, 139, 139));
        }

        private void SetSuccessStatus(string message)
        {
            StatusText.Text = message;
            StatusText.Foreground = new SolidColorBrush(Color.FromRgb(102, 187, 106));
        }

        private void SetErrorStatus(string message)
        {
            StatusText.Text = message;
            StatusText.Foreground = new SolidColorBrush(Color.FromRgb(239, 83, 80));
        }

        private bool IsInstallationValid()
        {
            return File.Exists(_gameExePath) && Directory.Exists(_resourcesFolderPath);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveNav("Home");
            ValidateInstallation();
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
            ValidateInstallation();

            if (!IsInstallationValid())
            {
                MessageBox.Show(
                    "The installation is incomplete. Please make sure SupremeGDPS.exe and the Resources folder are present in the launcher directory.",
                    "Installation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            try
            {
                SetNeutralStatus("• Launching game...");

                Process.Start(new ProcessStartInfo
                {
                    FileName = _gameExePath,
                    WorkingDirectory = _baseDirectory,
                    UseShellExecute = true
                });

                SetSuccessStatus("• Game launched successfully");
            }
            catch (Exception ex)
            {
                SetErrorStatus("• Launch failed");

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
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = _baseDirectory,
                    UseShellExecute = true
                });

                SetNeutralStatus("• Opened game directory");
            }
            catch (Exception ex)
            {
                SetErrorStatus("• Could not open game directory");

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

                SetNeutralStatus("• Opened AppData folder");
            }
            catch (Exception ex)
            {
                SetErrorStatus("• Could not open AppData folder");

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