using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebcamViewerX.ViewManagement
{
    public class Views
    {
        public View Home = new View()
        {
            Title = "Webcam Viewer",
            DevName = "Home",
            TitlebarBehavior = ViewTitlebarBehavior.ExtendsUnderTitlebar
        };


        public View Settings = new View()
        {
            Title = "Settings",
            DevName = "Settings",
            TitlebarBehavior = ViewTitlebarBehavior.ExtendsUnderTitlebar
        };
    }
}
