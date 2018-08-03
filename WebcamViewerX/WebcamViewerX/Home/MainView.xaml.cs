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

            // close the menu by default
            Menu.Animations = false;
            Menu.Close();
            Menu.Animations = true;

            anim_out_Icon_TextBlock.Visibility = Visibility.Hidden;

            MainWindow.titlebar.MenuButton_Click += MenuButtonClick;
            MainWindow.titlebar.BackButton_Click += BackButtonClick;
        }

        private void main_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                MainWindow.RequestTitlebarThemeChange("Dark"); // force dark titlebar theme because of dark titlebar tint
                MainWindow.titlebar.MenuButtonVisibility = Visibility.Visible;
            }
            else
                MainWindow.RequestTitlebarThemeChange(); // reset titlebar theme
        }

        #region Menu

        bool IsMenuOpen
        {
            get { return Menu.IsVisible; }
        }

        public void OpenMenu()
        {
            Menu.Open();
            anim_out_Icon_TextBlock.Visibility = Visibility.Visible;
        }

        public void CloseMenu()
        {
            Menu.Close();
            anim_out_Icon_TextBlock.Visibility = Visibility.Hidden;
        }

        void MenuButtonClick(object sender, RoutedEventArgs e)
        {
            OpenMenu();
            MainWindow.titlebar.MenuButtonVisibility = Visibility.Collapsed;
            MainWindow.titlebar.BackButtonVisibility = Visibility.Visible;
        }

        void BackButtonClick(object sender, RoutedEventArgs e)
        {
            CloseMenu();
            MainWindow.titlebar.MenuButtonVisibility = Visibility.Visible;
            MainWindow.titlebar.BackButtonVisibility = Visibility.Collapsed;
        }

        #endregion

        private void menu_localSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menu_archiveorgSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menu_bothSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menu_SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SwitchToView(MainWindow.Views.Settings);
        }
    }
}
