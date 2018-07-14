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
    public class PageGatherer
    {
        public Page GetViewPage(View view)
        {
            switch (view.DevName)
            {
                default:
                    return new Page();

                case "Home":
                    return new WebcamViewerX.Home.MainView();

                case "Settings":
                    return new Page()
                    {
                        Content = new TextBlock() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Text = "Settings", FontSize = 18 }
                    };
            }
        }
    }
}
