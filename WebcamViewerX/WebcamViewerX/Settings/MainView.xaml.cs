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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebcamViewerX.Configuration;

namespace WebcamViewerX.Settings
{
    public partial class MainView : Page
    {
        //ThemeManager ThemeManager;

        SubViewManager SubViewManager = new SubViewManager();
        SubViews SubViews = new SubViews();

        Config Config = Config.Default;
        MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;

        public MainView()
        {
            InitializeComponent();

            Config.PropertyChanged += Config_PropertyChanged;
        }

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            blurEffect.Radius = GetBlurAmount();
            Menu.GetNavigationMenu().SelectID(0);
        }

        private void main_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // temporary
            if (IsVisible)
            {
                MainWindow.titlebar.MenuButtonVisibility = Visibility.Collapsed;
                MainWindow.titlebar.BackButtonVisibility = Visibility.Collapsed;
                MainWindow.titlebar.Visibility = Visibility.Hidden;
            }
            else
            {
                MainWindow.titlebar.BackButtonVisibility = Visibility.Visible;
                MainWindow.titlebar.Visibility = Visibility.Visible;
            }
        }

        double GetBlurAmount()
        {
            switch (Config.blurfx)
            {
                default: return 35;
                case "full": return 10;
                case "lite": return 8;
                case "off": return 0;
            }
        }

        private void Config_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            blurEffect.Radius = GetBlurAmount();
        }

        private void NavigationMenu_SelectionChanged(object sender, EventArgs e)
        {
            int? id = Menu.GetNavigationMenu().CurrentSelection;
            if (id != null)
            {
                SwitchToSubView(SubViews.GetSubViewFromID(id.Value));
                //Appbar.Title = CurrentSubView.Page.Title.ToString();
            }
            else
            { /* error! */ }
        }

        #region Subview management

        // Based on the main View management system

        public SubView CurrentSubView;

        public void SwitchToSubView(SubView subview)
        {
            subview = SubViewManager.GetSubView(subview);

            Frame frame = null;
            string frame_name = subview.DevName + "_Frame";

            foreach (Frame containedFrame in FramesContainer.Children)
            {
                if (containedFrame.Name == frame_name)
                    frame = containedFrame;
            }

            if (frame == null)
            {
                frame = new Frame() { Name = frame_name, Opacity = 0 };
                FramesContainer.Children.Add(frame);
            }

            if (frame.Content == null)
                frame.Content = subview.Page;

            RequestFrameVisiblity(frame_name);

            CurrentSubView = subview;
        }

        public async void RequestFrameVisiblity(string frameName)
        {
            foreach (Frame visframe in FramesContainer.Children)
            {
                DoubleAnimation anim_in = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(.3));
                DoubleAnimation anim_out = new DoubleAnimation(0, TimeSpan.FromSeconds(.2));

                if (visframe.Name != frameName)
                {
                    if (visframe.Opacity != 0)
                    {
                        visframe.BeginAnimation(OpacityProperty, anim_out);
                        await Task.Delay(TimeSpan.FromSeconds(.2));
                        visframe.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    visframe.Visibility = Visibility.Visible;
                    visframe.BeginAnimation(OpacityProperty, anim_in);
                }
            }
        }

        #endregion

        private void menu_backButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SwitchToView(MainWindow.Views.Home);
        }
    }
}
