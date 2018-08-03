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

namespace WebcamViewerX.Settings.Subviews
{
    public partial class UserInterfaceSubView : Page
    {
        MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;
        Theming.ThemeManager ThemeManager;

        public UserInterfaceSubView()
        {
            InitializeComponent();
            ThemeManager = new Theming.ThemeManager(themeDictionary);
        }

        XeZrunner.UI.Configuration.Config Config = XeZrunner.UI.Configuration.Config.Default;
        Theming.Config Theme_Config = Theming.Config.Default;

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            switch (Theme_Config.theme)
            {
                case "Light":
                    theme_0.IsChecked = true; break;
                case "Dark":
                    theme_1.IsChecked = true; break;
            }

            switch (Config.controlfx)
            {
                case "P":
                    controlfx_P.IsChecked = true; break;
                case "MM":
                    controlfx_MM.IsChecked = true; break;
                case "Reveal":
                    controlfx_Reveal.IsChecked = true; break;
            }
        }

        void ValidateThemeChanges()
        {
            foreach (RadioButton button in themeStackPanel.Children)
            {
                if (button.IsChecked.Value)
                    Theme_Config.theme = (string)button.Content;
            }
        }

        void ValidateControlFXChanges()
        {
            foreach (RadioButton button in controlfxStackPanel.Children)
            {
                if (button.IsChecked.Value)
                    Config.controlfx = (string)button.Content;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Theme_Config.Save();
            Config.Save();

            MainWindow.ShowTextDialog("Changes saved",
                "Current config values: \n\n" +
                "theme: " + Theme_Config.theme + "\n" +
                "accent: " + "" + "\n" +
                "controlfx: " + Config.controlfx + "\n"
                );
        }

        private void theme_Click(object sender, RoutedEventArgs e)
        {
            ValidateThemeChanges();
        }

        private void controlfx_Click(object sender, RoutedEventArgs e)
        {
            ValidateControlFXChanges();
        }
    }
}
