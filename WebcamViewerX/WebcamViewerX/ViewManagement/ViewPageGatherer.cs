using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WebcamViewerX.ViewManagement
{
    /// <summary>
    /// This class serves its purpose to gather the Page objects for the Views.
    /// It's a seperate class so the ViewManager.cs doesn't get messy.
    /// </summary>
    public class ViewPageGatherer
    {
        MainWindow mainwindow;

        public Page GetViewPage(View view)
        {
            if (mainwindow == null)
                mainwindow = (MainWindow)Application.Current.MainWindow;

            Page pageToReturn;

            switch (view.DevName)
            {
                default:
                    pageToReturn = new Page();
                    break;

                case "Home":
                    pageToReturn = new Home.MainView();
                    break;

                case "Settings":
                    pageToReturn = new Settings.MainView();
                    break;
            }

            switch (view.TitlebarBehavior)
            {
                // push margin to titlebar height
                case ViewTitlebarBehavior.ExtendsToTitlebar:
                case ViewTitlebarBehavior.ExtendsUnderTitlebar:
                    pageToReturn.Margin = new Thickness(0, -mainwindow.titlebar.Height, 0, 0);
                    break;
            }

            return pageToReturn;
        }
    }
}
