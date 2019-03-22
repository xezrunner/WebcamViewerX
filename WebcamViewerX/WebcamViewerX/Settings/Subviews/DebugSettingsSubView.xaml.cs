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
    public partial class DebugSettingsSubView : Page
    {
        MainWindow mainwindow = (MainWindow)Application.Current.MainWindow;
        XeZrunner.UI.Theming.ThemeManager ThemeManager;

        public DebugSettingsSubView()
        {
            InitializeComponent();

            ThemeManager = new XeZrunner.UI.Theming.ThemeManager(themeDictionary);
        }

        private async void AnimscaleButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog() { Title = "Chagne animation scale", PrimaryButtonText = "Change", SecondaryButtonText = "Cancel" };
            TextBox box = new TextBox() { Text = mainwindow.animation_scale.ToString() };
            dialog.Content = box;
            if (await mainwindow.contentdialogHost.ShowDialogAsync(dialog) == ContentDialogHost.ContentDialogResult.Primary)
            {
                try
                {
                    mainwindow.animation_scale = double.Parse(box.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
    }
}
