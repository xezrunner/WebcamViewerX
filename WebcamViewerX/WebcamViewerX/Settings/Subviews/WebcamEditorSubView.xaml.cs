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
    public partial class WebcamEditorSubView : Page
    {
        XeZrunner.UI.Theming.ThemeManager ThemeManager;

        public WebcamEditorSubView()
        {
            InitializeComponent();
            ThemeManager = new XeZrunner.UI.Theming.ThemeManager(themeDictionary);
        }

        MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;
        Engine.CameraConfiguration CameraConfig = new Engine.CameraConfiguration();

        private void getconfigButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowTextDialog("JSON user configuration debug", CameraConfig.Debug_GetUserCamerasAsString());
        }
    }
}
