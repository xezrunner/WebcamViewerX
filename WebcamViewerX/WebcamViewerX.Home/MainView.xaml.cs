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

        #region Theming

        public void ThemeChangeHandler(object sender, EventArgs e)
        {
            themeDictionary.MergedDictionaries.Clear();
            themeDictionary.MergedDictionaries.Add((ResourceDictionary)sender);
        }

        #endregion
    }
}
