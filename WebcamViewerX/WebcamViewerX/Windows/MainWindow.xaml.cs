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
using System.Windows.Media.Animation;
using XeZrunner.UI.Popups;
using XeZrunner.UI.Theming;
using MahApps.Metro.Controls;
using System.Windows.Interop;

namespace WebcamViewerX
{
    public partial class MainWindow : MetroWindow
    {
        public ThemeManager ThemeManager;
        ViewManager ViewManager = new ViewManager();
        public Views Views = new Views();
        Configuration.Config Config = Configuration.Config.Default;

        public MainWindow()
        {
            // MUI debug
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("hu");

            // Exception handling
            Application.Current.Dispatcher.UnhandledException += Dispatcher_UnhandledException;

            ThemeManager = new ThemeManager(Application.Current.Resources);
            ThemeManager.ConfigChanged += ThemeManager_ConfigChanged;

            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            SwitchToView(Views.Home);
        }

        #region View management

        public double animation_scale = 1.0;

        public View CurrentView;

        /// <summary>
        /// This changes the current View to the desired View.
        /// </summary>
        /// <param name="view"></param>
        public async void SwitchToView(View view)
        {
            frameContainer.Visibility = Visibility.Visible;

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
                frame = new Frame() { Name = frame_name, Visibility = Visibility.Visible };
                frameContainer.Children.Add(frame);
            }

            // Frame Content
            if (frame.Content == null) // if we were to check if the Frame's Content is the same as the View's Page object, there would be a noticable delay, or at least on my system.
                frame.Content = view.Page;

            if (CurrentView != null)
                await CurrentView.RequestAnimOutAnimation(animation_scale);

            // Set CurrentView to our new View
            CurrentView = view;

            // Set Frame to be visible.
            //RequestFrameVisibility(frame_name);

            await view.RequestAnimInAnimation(animation_scale);
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
            if (!Keyboard.IsKeyDown(Key.LeftCtrl))
                return;
            // Themes
            if (e.Key == Key.Q)
                ThemeManager.Config_SetTheme(XeZrunner.UI.Theming.ThemeManager.Theme.Light);

            if (e.Key == Key.E)
                ThemeManager.Config_SetTheme(XeZrunner.UI.Theming.ThemeManager.Theme.Dark);

            // View changing
            if (e.Key == Key.Y)
                SwitchToView(Views.Home);

            if (e.Key == Key.X)
                SwitchToView(Views.Settings);

            // Popup test
            if (e.Key == Key.C)
                ShowTextDialog("ContentDialog testing", "Hello!");
        }

        #endregion

        #region Button click events
        private void titlebar_MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuButtonClick?.Invoke(sender, e);
        }

        private void titlebar_BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClick?.Invoke(sender, e);
        }
        #endregion

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ShowTextDialog("An error has occured.",
                "You may continue using the application, but it may no longer work as intended.\n\n" +
                "Error: " + e.Exception.Message);
            e.Handled = true;
        }

        public void ShowTextDialog(string Text)
        {
            ShowTextDialog("", Text);
        }

        public async void ShowTextDialog(string Title, string Text)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = Title,
                Content = Text,
                PrimaryButtonText = "OK",
                SecondaryButtonText = ""
            };
            await contentdialogHost.ShowDialogAsync(dialog);
        }

        #endregion

        RenderTargetBitmap Screenshot(FrameworkElement element)
        {
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(element);

            //await Task.Delay(1);

            return renderTargetBitmap;
        }

        public async void ThemeManager_ConfigChanged(object sender, EventArgs e)
        {
            themechangeImage.Source = Screenshot(this);

            themechangeImage.Visibility = Visibility.Visible;

            DoubleAnimation anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(.3));
            themechangeImage.BeginAnimation(OpacityProperty, anim);

            RequestTitlebarThemeChange(ThemeManager.GetCurrentConfigTheme().ToString());

            await Task.Delay(TimeSpan.FromSeconds(.3));
            themechangeImage.Visibility = Visibility.Hidden;
        }

        double GetBlurAmount()
        {
            switch (Config.blurfx)
            {
                default: return 35;
                case "full": return 35;
                case "lite": return 15;
                case "off": return 0;
            }
        }
        private async void Titlebar_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            double anim_duration = 0.35;

            DoubleAnimation bluranim_in = new DoubleAnimation(GetBlurAmount(), TimeSpan.FromSeconds(anim_duration));
            DoubleAnimation opacity_in = new DoubleAnimation(1, TimeSpan.FromSeconds(anim_duration));

            DoubleAnimation bluranim_out = new DoubleAnimation(0, TimeSpan.FromSeconds(anim_duration));
            DoubleAnimation opacity_out = new DoubleAnimation(0, TimeSpan.FromSeconds(anim_duration));

            if (titlebarContextPanel.IsVisible != true)
            {
                titlebarContextPanel.Visibility = Visibility.Visible;

                titlebar_context_background_blur.BeginAnimation(System.Windows.Media.Effects.BlurBitmapEffect.RadiusProperty, bluranim_in);
                titlebar_context_innerpanel.BeginAnimation(OpacityProperty, opacity_in);
            }
            else
            {
                titlebar_context_background_blur.BeginAnimation(System.Windows.Media.Effects.BlurBitmapEffect.RadiusProperty, bluranim_out);
                titlebar_context_innerpanel.BeginAnimation(OpacityProperty, opacity_out);

                await Task.Delay(TimeSpan.FromSeconds(anim_duration));

                titlebarContextPanel.Visibility = Visibility.Hidden;
            }
        }
    }
}