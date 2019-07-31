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
using WebcamViewerX.Configuration.FeatureControl;
using XeZrunner.UI.Popups;

namespace WebcamViewerX.Settings.Subviews
{
    public partial class DebugSettingsSubView : Page
    {
        MainWindow mainwindow = (MainWindow)Application.Current.MainWindow;
        XeZrunner.UI.Theming.ThemeManager ThemeManager;

        public DebugSettingsSubView()
        {
            InitializeComponent();
        }

        private async void AnimscaleButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog() { Title = "Chagne animation scale", PrimaryButtonText = "Change", SecondaryButtonText = "Cancel" };
            TextBox box = new TextBox() { Text = mainwindow.animation_scale.ToString() };
            dialog.Content = box;
            if (await mainwindow.contentdialogHost.ShowDialogAsync(dialog) == ContentDialogHost.ContentDialogResult.Primary)
            {
                try
                {
                    mainwindow.animation_scale = double.Parse(box.Text);
                }
                catch (Exception ex)
                {
                    mainwindow.contentdialogHost.TextContentDialog("", ex.Message, true);
                }
            }
        }

        private void FCDebugReadButton_Click(object sender, RoutedEventArgs e)
        {
            FeatureControlManager man = new FeatureControlManager();
            ContentDialog dialog = new ContentDialog() { Title = "Feature Control Debug", PrimaryButtonText = "OK" };

            string featuresString = "";
            foreach (FeatureGroup group in man.GetFeatureGroups())
            {
                featuresString += "Universe: " + group.Universe + "\n";
                foreach (Feature feature in group.Features)
                {
                    featuresString += String.Format("{0} [{1}] ({2}): {3}\n", feature.Name, feature.DevName, feature.Category, feature.Value);
                }
                featuresString += "\n";
            }

            string finalString = string.Format("Features:\n\n{0}------------------------------\n\nRaw JSON:\n{1}", featuresString, man.ReadJSON());
            dialog.Content = finalString;

            mainwindow.contentdialogHost.ShowDialog(dialog);
        }

        private void FCDebugCreateButton_Click(object sender, RoutedEventArgs e)
        {
            FeatureControlManager man = new FeatureControlManager();
            man.DEBUG_CreateFeatureConfig();

            mainwindow.contentdialogHost.TextContentDialog("Feature Control Debug", "Feature Control File created successfully.\n\nPath: " + man.GetPathToJSON() + "\n");
        }

        private async void FCChangeFeatureButton_Click(object sender, RoutedEventArgs e)
        {
            FeatureControlManager man = new FeatureControlManager();
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Feature Control Debug",
                PrimaryButtonText = "Change",
                SecondaryButtonText = "Cancel"
            };

            /* Create panel */
            StackPanel panel = new StackPanel();

            panel.Children.Add(new TextBlock { Text = "Universe" });

            TextBox universeBox = new TextBox();
            panel.Children.Add(universeBox);

            panel.Children.Add(new TextBlock { Text = "Feature (friendly or DevName)" });

            TextBox featurenameBox = new TextBox();
            panel.Children.Add(featurenameBox);

            panel.Children.Add(new TextBlock { Text = "New value" });

            TextBox newvalueBox = new TextBox();
            panel.Children.Add(newvalueBox);

            dialog.Content = panel;

            if (await mainwindow.contentdialogHost.ShowDialogAsync(dialog) == ContentDialogHost.ContentDialogResult.Primary)
            {
                try
                { man.ChangeFeatureValue(universeBox.Text, featurenameBox.Text, newvalueBox.Text); }
                catch (Exception ex)
                { mainwindow.contentdialogHost.TextContentDialog("", ex.Message, IsErrorDialog:true); }
            }
        }
    }
}
