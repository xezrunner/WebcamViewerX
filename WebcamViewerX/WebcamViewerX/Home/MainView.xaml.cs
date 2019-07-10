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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebcamViewerX.Configuration;
using WebcamViewerX.Configuration.CameraConfiguration;
using WebcamViewerX.Engine;
using WebcamViewerX.ViewManagement;
using XeZrunner.UI.Controls;
using XeZrunner.UI.Controls.Buttons;

namespace WebcamViewerX.Home
{
    public partial class MainView : Page
    {
        XeZrunner.UI.Utilities.UIDTools UIDTools = new XeZrunner.UI.Utilities.UIDTools();
        public XeZrunner.UI.Theming.ThemeManager ThemeManager;

        CameraConfigurationManager CameraConfig = new CameraConfigurationManager();
        CameraBitmapImageGatherer ImageGatherer = new CameraBitmapImageGatherer();
        ImageCameraSaveUtils LocalSaveUtils = new ImageCameraSaveUtils();
        ArchiveOrgUtils ArchiveorgUtils = new ArchiveOrgUtils();

        MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;

        public MainView()
        {
            InitializeComponent();

            ThemeManager = new XeZrunner.UI.Theming.ThemeManager(themeDictionary); // initialize theme manager

            navMenu = (NavigationMenu)GetMenuUIElementFromUid("Menu_NavigationMenu");
        }

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            // close the menu by default
            Menu.Animations = false;
            Menu.Close();
            Menu.Animations = true;

            anim_out_Icon_TextBlock.Visibility = Visibility.Hidden;

            MainWindow.titlebar.MenuButton_Click += MenuButtonClick;
            MainWindow.titlebar.BackButton_Click += BackButtonClick;

            LoadConfiguration();
        }

        private async void main_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                MainWindow.RequestTitlebarThemeChange(null);
                MainWindow.titlebar.BackButtonVisibility = Visibility.Visible;

                DoubleAnimation anim_blur_menu = new DoubleAnimation(35, TimeSpan.FromSeconds(.35));

                await Task.Delay(TimeSpan.FromSeconds(0.5));

                Menu_BlurBehind.BeginAnimation(BlurEffect.RadiusProperty, anim_blur_menu);
            }
            else
                MainWindow.RequestTitlebarThemeChange(MainWindow.ThemeManager.GetCurrentConfigTheme().ToString()); // reset titlebar theme
        }

        #region Configuration

        List<Camera> _cameraList;

        void LoadConfiguration()
        {
            _cameraList = CameraConfig.GetUserCameras();

            // Create "Cameras" StackPanel
            StackPanel camerasPanel = new StackPanel() { Tag = "Cameras" };

            foreach (Camera camera in _cameraList)
                camerasPanel.Children.Add(Menu_CreateNavMenuItem(camera));

            navMenu.Items.Clear();

            navMenu.Items.Add(camerasPanel);
        }

        NavMenuItem Menu_CreateNavMenuItem(Camera camera)
        {
            return new NavMenuItem()
            {
                Text = camera.Name,
                Icon = "",
                Height = 35
            };
        }

        #endregion

        #region Window & titlebar

        void MenuButtonClick(object sender, RoutedEventArgs e)
        {
            OpenMenu();
        }

        void BackButtonClick(object sender, RoutedEventArgs e)
        {
            CloseMenu();
        }

        #endregion

        #region Menu

        NavigationMenu navMenu;
        UIElement GetMenuUIElementFromUid(string uid)
        {
            return UIDTools.GetUIElementByUid(Menu, uid);
        }

        bool IsMenuOpen
        {
            get { return Menu.IsVisible; }
        }

        public void OpenMenu()
        {
            DoubleAnimation blur_in = new DoubleAnimation(35, TimeSpan.FromSeconds(.35));
            Menu_BlurBehind.BeginAnimation(BlurEffect.RadiusProperty, blur_in);

            Menu.Open();
            anim_out_Icon_TextBlock.Visibility = Visibility.Visible;

            MainWindow.titlebar.MenuButtonVisibility = Visibility.Collapsed;
            MainWindow.titlebar.BackButtonVisibility = Visibility.Visible;
        }

        public void CloseMenu()
        {
            DoubleAnimation blur_in = new DoubleAnimation(0, TimeSpan.FromSeconds(.25));
            Menu_BlurBehind.BeginAnimation(BlurEffect.RadiusProperty, blur_in);

            Menu.Close();
            anim_out_Icon_TextBlock.Visibility = Visibility.Hidden;

            MainWindow.titlebar.MenuButtonVisibility = Visibility.Visible;
            MainWindow.titlebar.BackButtonVisibility = Visibility.Collapsed;
        }

        #region Info grid

        void SetInfoGrid(Camera camera)
        {
            TextBlock infogridTextBlock = (TextBlock)GetMenuUIElementFromUid("Menu_infogridTextBlock");

            infogridTextBlock.Inlines.Clear();

            infogridTextBlock.Inlines.Add(new Run() { Text = camera.Name, FontWeight = FontWeights.Medium });
            infogridTextBlock.Inlines.Add(new LineBreak());

            infogridTextBlock.Inlines.Add(camera.URL);
            infogridTextBlock.Inlines.Add(new LineBreak());

            infogridTextBlock.Inlines.Add("Owner: " + camera.Owner);
            infogridTextBlock.Inlines.Add(new LineBreak());

            infogridTextBlock.Inlines.Add("Location: " + camera.Location);
        }

        #endregion

        #region Info grid action buttons

        private async void menu_localSaveButton_Click(object sender, RoutedEventArgs e)
        {
            cameraView.IsLoading = true;
            await LocalSaveUtils.SaveImageFile(_cameraList[navMenu.CurrentSelection.Value]);
            cameraView.IsLoading = false;
        }

        private async void menu_archiveorgSaveButton_Click(object sender, RoutedEventArgs e)
        {
            cameraView.IsLoading = true;
            await ArchiveorgUtils.SaveOnArchiveOrg(_cameraList[navMenu.CurrentSelection.Value]);
            cameraView.IsLoading = false;
        }

        private void menu_bothSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Middle

        private async void Menu_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                await LoadImage(_cameraList[navMenu.CurrentSelection.Value]);
            }
            catch (Exception ex)
            {
                MainWindow.contentdialogHost.TextContentDialog("Could not load image", ex.Message);
            }
        }

        #endregion

        #region Bottom

        private void menu_SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation anim_blur_menu = new DoubleAnimation(0, TimeSpan.FromSeconds(.35));
            Menu_BlurBehind.BeginAnimation(BlurEffect.RadiusProperty, anim_blur_menu);

            MainWindow.SwitchToView(MainWindow.Views.Settings);
        }

        #endregion

        #endregion

        #region Engine

        async Task LoadImage(Camera camera)
        {
            cameraView.IsLoading = true;
            cameraView.Image = await ImageGatherer.GetCameraImage(camera);
            cameraView.IsLoading = false;

            SetInfoGrid(camera);

            CloseMenu();
        }

        #endregion

        private void cameraView_Click(object sender, RoutedEventArgs e)
        {
            Menu_SelectionChanged(sender, null);
        }
    }
}
