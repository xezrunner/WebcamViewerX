using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Runtime.InteropServices;
using WebcamViewerX.ViewManagement;

namespace WebcamViewerX
{
    public partial class MainWindow : Window
    {
        Theming.ThemeManager ThemeManager;
        ViewManager ViewManager = new ViewManager();
        Views Views = new Views();

        public MainWindow()
        {
            // MUI debug
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");

            InitializeComponent();

            ThemeManager = new Theming.ThemeManager(themeDictionary); // initialize theme manager
        }

        #region View management

        public View CurrentView;

        /// <summary>
        /// This changes the current View to the desired View.
        /// </summary>
        /// <param name="view"></param>
        public void SwitchToView(View view)
        {
            // TODO: remove temp
            temp_scrollviewer.Visibility = Visibility.Collapsed;

            view = ViewManager.GetView(view);

            Frame frame = null;
            string frame_name = view.DevName + "_Frame";

            // Try to get the Frame for the View.
            // If there's no Frame, create one.
            foreach (Frame containedFrame in frameContainer.Children)
            {
                if (containedFrame.Name == frame_name)
                    frame = containedFrame;
            }

            if (frame == null)
            {
                frame = new Frame() { Name = frame_name };
                frameContainer.Children.Add(frame);
            }

            // Frame Content
            if (frame.Content == null) // if we were to check if the Frame's Content is the same as the View's Page object, there would be a noticable delay, or at least on my system.
                frame.Content = view.Page;

            // Set Frame to be visible.
            RequestFrameVisibility(frame_name);

            // Set CurrentView to our new View
            CurrentView = view;
        }

        /// <summary>
        /// This cycles through the frames in the Frames container grid, and handles visibility depending on the given Frame target name.
        /// </summary>
        /// <param name="frameName">The target frame.</param>
        public void RequestFrameVisibility(string frameName)
        {
            foreach (Frame visframe in frameContainer.Children)
            {
                if (visframe.Name != frameName)
                    visframe.Visibility = Visibility.Hidden;
                else
                    visframe.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region Window & Titlebar

        #region Window events

        public event RoutedEventHandler MenuButtonClick;
        public event RoutedEventHandler BackButtonClick;

        public void RequestTitleChange(string title)
        {
            titlebar.AppTitle = title;
        }

        /// <summary>
        /// Change the titlebar theme.
        /// </summary>
        /// <param name="theme">The theme to change to. (use null to restore to global theme)</param>
        public void RequestTitlebarThemeChange(string theme = null)
        {
            if (theme == null)
                titlebar.Theme = null;
            if (theme == "Light")
                titlebar.Theme = XeZrunner.UI.Theming.ThemeManager.Theme.Light;
            if (theme == "Dark")
                titlebar.Theme = XeZrunner.UI.Theming.ThemeManager.Theme.Dark;
        }

        // temporary debug stuff
        private void window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Themes
            if (e.Key == Key.Q)
                ThemeManager.Config_SetTheme(Theming.ThemeManager.Theme.Light);

            if (e.Key == Key.E)
                ThemeManager.Config_SetTheme(Theming.ThemeManager.Theme.Dark);

            // View changing
            if (e.Key == Key.Y)
                SwitchToView(Views.Home);

            if (e.Key == Key.X)
                SwitchToView(Views.Settings);
        }

        #endregion

        #region Button click events
        private void titlebar_MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuButtonClick?.Invoke(sender ,e);
        }

        private void titlebar_BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClick?.Invoke(sender, e);
        }
        #endregion

        #endregion
    }
}