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
using WebcamViewerX.Configuration;
using WebcamViewerX.Configuration.CameraConfiguration;

namespace WebcamViewerX.Settings.Subviews
{
    public partial class WebcamEditorSubView : Page
    {
        XeZrunner.UI.Theming.ThemeManager ThemeManager;

        public WebcamEditorSubView()
        {
            InitializeComponent();
        }

        MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;
        CameraConfigurationManager CameraConfig = new CameraConfigurationManager();

        private void getconfigButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowTextDialog("JSON user configuration debug", CameraConfig.Debug_GetUserCamerasAsString());
        }
    }
}
