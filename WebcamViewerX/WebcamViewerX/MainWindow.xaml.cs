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

namespace WebcamViewerX
{
    public partial class MainWindow : Window
    {
        Theming.ThemeManager ThemeManager;

        public MainWindow()
        {
            InitializeComponent();

            ThemeManager = new Theming.ThemeManager(themeDictionary); // initialize theme manager
        }

        // TODO: ThemeManager: make improvements, including Accent colors.
        // Right now, this is pretty much copy-paste from XesignPhotos.ThemeManager, and that app had/has a single accent color to worry about.
        // We're to be keeping all of the main UI from Webcam Viewer "9" (overviewy), but improving it *a lot*.
        // That means we're keeping the accent color choices, and perhaps we're even adding a third Black theme, as the new theme engine actually supports more than 2 themes already, theoretically.
        // I've had an idea for event-specific themes, such as for winter, Christmas etc... aswell..
        #region Theming

        public void ThemeChangeHandler(object sender, EventArgs e)
        {
            themeDictionary.MergedDictionaries.Clear();
            themeDictionary.MergedDictionaries.Add((ResourceDictionary)sender);
        }

        #endregion

        private void window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // temporary debug stuff

            if (e.Key == Key.Q)
                ThemeManager.Config_SetTheme(Theming.ThemeManager.Theme.Light);

            if (e.Key == Key.E)
                ThemeManager.Config_SetTheme(Theming.ThemeManager.Theme.Dark);
        }
    }
}
