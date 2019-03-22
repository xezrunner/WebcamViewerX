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
    public partial class UserInterface2SubView : Page
    {
        MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;
        XeZrunner.UI.Theming.ThemeManager ThemeManager;

        public UserInterface2SubView()
        {
            InitializeComponent();
        }

        private void main_Loaded(object sender, RoutedEventArgs e)
        {

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
    }
}
