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
using XeZrunner.UI.Popups;

namespace WebcamViewerX.Settings.Subviews
{
    public partial class UserInterfaceSubView : Page
    {
        MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;
        XeZrunner.UI.Theming.ThemeManager ThemeManager;

        public UserInterfaceSubView()
        {
            InitializeComponent();
            ThemeManager = new XeZrunner.UI.Theming.ThemeManager(themeDictionary);
        }

        XeZrunner.UI.Configuration.Config Config = XeZrunner.UI.Configuration.Config.Default;
        XeZrunner.UI.Theming.Config Theme_Config = XeZrunner.UI.Theming.Config.Default;

        bool _isLoaded;

        private void main_Loaded(object sender, EventArgs e)
        {
            _isLoaded = false;

            switch (Theme_Config.theme)
            {
                case "Light":
                    themeToggleButton.IsActive = false; break;
                case "Dark":
                    themeToggleButton.IsActive = true; break;
            }

            switch (Config.controlfx)
            {
                case "P":
                    controlfx_P.IsActive = true; break;
                case "MM":
                    controlfx_MM.IsActive = true; break;
                case "Reveal":
                    controlfx_Reveal.IsActive = true; break;
            }

            foreach (XeZrunner.UI.Controls.RadioButton button in accentStackPanel.Children)
                if ((string)button.Text == Theme_Config.accent)
                    button.IsActive = true;

            _isLoaded = true;
        }

        void ValidateThemeChanges()
        {
            if (!_isLoaded)
                return;

            /*
            foreach (XeZrunner.UI.Controls.RadioButton button in themeStackPanel.Children)
            {
                if (button.IsActive)
                    Theme_Config.theme = (string)button.Text;
            }
            */
            Theme_Config.theme = themeToggleButton.IsActive ? "Dark" : "Light";
        }

        void ValidateControlFXChanges()
        {
            if (!_isLoaded)
                return;

            int counter = 0;
            foreach (XeZrunner.UI.Controls.RadioButton button in controlfxStackPanel.Children)
            {
                if (button.IsActive)
                {
                    if (counter == 0)
                        Config.controlfx = "P";
                    if (counter == 1)
                        Config.controlfx = "MM";
                    if (counter == 2)
                        Config.controlfx = "Reveal";

                    break;
                }
                counter++;
            }
        }

        void ValidateAccentChanges()
        {
            if (!_isLoaded)
                return;

            foreach (XeZrunner.UI.Controls.RadioButton button in accentStackPanel.Children)
            {
                if (button.IsActive)
                    Theme_Config.accent = (string)button.Text;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Theme_Config.Save();
            Config.Save();

            MainWindow.ShowTextDialog("Changes saved",
                "Current config values: \n\n" +
                "theme: " + Theme_Config.theme + "\n" +
                "accent: " + "" + Theme_Config.accent + "\n" +
                "controlfx: " + Config.controlfx + "\n"
                );
        }

        private void theme_Click(object sender, EventArgs e)
        {
            ValidateThemeChanges();
            Theme_Config.Save();
        }

        private void controlfx_Click(object sender, EventArgs e)
        {
            ValidateControlFXChanges();
            Theme_Config.Save();
        }

        private void accent_Click(object sender, EventArgs e)
        {
            ValidateAccentChanges();
            Theme_Config.Save();
        }

        private async void resetConfigButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Reset theming engine",
                Content = "Resetting the theming engine will return the following settings to their defaults: \n\n" +
                "Theme: Light\n" +
                "Accent color: Blue\n" +
                "Control effects: Reveal Highlight",

                PrimaryButtonText = "Reset configuration",
                SecondaryButtonText = "Cancel"
            };

            if (await MainWindow.contentdialogHost.ShowDialogAsync(dialog) == ContentDialogHost.ContentDialogResult.Primary)
            {
                ThemeManager.Config.Reset();
                ThemeManager.Config.Save();

                // reload this page
                main_Loaded(this, null);
            }
        }

        private void ThemeToggleButton_IsActiveChanged(object sender, EventArgs e)
        {
            ValidateThemeChanges();
        }
    }
}
