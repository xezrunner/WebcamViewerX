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
                    return new Home.MainView();

                case "Settings":
                    return new Settings.MainView();
            }
        }
    }
}
