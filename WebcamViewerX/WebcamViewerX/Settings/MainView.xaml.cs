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

namespace WebcamViewerX.Settings
{
    public partial class MainView : Page
    {
        Theming.ThemeManager ThemeManager;

        public MainView()
        {
            InitializeComponent();

            ThemeManager = new Theming.ThemeManager(themeDictionary); // initialize theme manager
        }

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void NavigationMenu_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Menu.Close();
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            Menu.Open();
        }

        private void normalButton_Click(object sender, RoutedEventArgs e)
        {
            Menu.Mode = XeZrunner.UI.Controls.MenuControl.MenuMode.Normal;
        }

        private void compactButton_Click(object sender, RoutedEventArgs e)
        {
            Menu.Mode = XeZrunner.UI.Controls.MenuControl.MenuMode.Compact;
        }
    }
}
