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
using WebcamViewerX.Hooks;

namespace WebcamViewerX.Home
{
    public partial class MainView : Page
    {
        Theming.ThemeManager ThemeManager;

        public MainView()
        {
            InitializeComponent();

            ThemeManager = new Theming.ThemeManager(themeDictionary); // initialize theme manager
        }

        MainWindowHooks hooks;

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            SetupHooks();
        }

        void SetupHooks()
        {
            hooks = (MainWindowHooks)this.Tag;

            hooks.TitlebarHooks.MenuButtonClick += Titlebar_MenuButtonClick;
            hooks.TitlebarHooks.BackButtonClick += Titlebar_BackButtonClick;
        }

        #region Private variables
        double anim_speedratio;
        #endregion

        #region Theming

        public void ThemeChangeHandler(object sender, EventArgs e)
        {
            themeDictionary.MergedDictionaries.Clear();
            themeDictionary.MergedDictionaries.Add((ResourceDictionary)sender);
        }

        #endregion

        #region Menu

        bool IsMenuOpen
        {
            get { return grid_Menu.IsVisible; }
        }

        public void OpenMenu()
        {
            /*
            Storyboard board = (Storyboard)FindResource("Menu_In");
            board.Begin(); board.SetSpeedRatio(anim_speedratio);
            */
        }

        public void CloseMenu()
        {
            /*
            Storyboard board = (Storyboard)FindResource("Menu_Out");
            board.Begin(); board.SetSpeedRatio(anim_speedratio);
            */
        }

        #region Click events
        private void Titlebar_MenuButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Home: Opening menu!");
            OpenMenu();
        }

        private void Titlebar_BackButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Home: Closing menu!");
            CloseMenu();
        }
        #endregion

        #endregion
    }
}
