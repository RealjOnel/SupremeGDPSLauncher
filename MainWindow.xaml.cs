using System.Windows;
using SupremeGDPSLauncher.Views;

namespace SupremeGDPSLauncher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadPage("Home");
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
                    break;

                case "Updates":
                    UpdatesButton.Style = (Style)FindResource("ActiveNavButtonStyle");
                    break;

                case "Features":
                    FeaturesButton.Style = (Style)FindResource("ActiveNavButtonStyle");
                    break;

                case "Settings":
                    SettingsButton.Style = (Style)FindResource("ActiveNavButtonStyle");
                    break;
            }
        }

        private void LoadPage(string pageName)
        {
            SetActiveNav(pageName);

            switch (pageName)
            {
                case "Home":
                    MainContent.Content = new HomeView();
                    break;

                case "Updates":
                    MainContent.Content = new UpdatesView();
                    break;

                case "Features":
                    MainContent.Content = new FeaturesView();
                    break;

                case "Settings":
                    MainContent.Content = new SettingsView();
                    break;
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("Home");
        }

        private void UpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("Updates");
        }

        private void FeaturesButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("Features");
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("Settings");
        }
    }
}