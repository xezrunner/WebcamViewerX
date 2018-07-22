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
using WebcamViewerX.ViewManagement;

namespace WebcamViewerX.Home
{
    public partial class MainView : Page
    {
        Theming.ThemeManager ThemeManager;

        View View;
        MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;

        public MainView()
        {
            InitializeComponent();

            ThemeManager = new Theming.ThemeManager(themeDictionary); // initialize theme manager
        }

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            View = (View)this.Tag;
        }

        private void main_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
                MainWindow.RequestTitlebarThemeChange("Dark"); // force dark titlebar theme because of dark titlebar tint
            else
                MainWindow.RequestTitlebarThemeChange(); // reset titlebar theme
        }

        #region Menu

        bool IsMenuOpen
        {
            get { return grid_Menu.IsVisible; }
        }

        public void OpenMenu()
        {
            MessageBox.Show("Debug: OpenMenu()!");
            /*
            Storyboard board = (Storyboard)FindResource("Menu_In");
            board.Begin(); board.SetSpeedRatio(anim_speedratio);
            */
        }

        public void CloseMenu()
        {
            MessageBox.Show("Debug: CloseMenu()!");
            /*
            Storyboard board = (Storyboard)FindResource("Menu_Out");
            board.Begin(); board.SetSpeedRatio(anim_speedratio);
            */
        }

        #endregion
    }
}
