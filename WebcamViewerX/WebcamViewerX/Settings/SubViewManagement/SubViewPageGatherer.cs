using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WebcamViewerX.Settings
{
    public class SubViewPageGatherer
    {
        public Page GetSubViewPage(SubView subview)
        {
            switch (subview.DevName)
            {
                default:
                    return new Page();

                case "WebcamEditor":
                    return new Subviews.WebcamEditorSubView();

                case "HomeSettings":
                    return new Subviews.HomeSettingsSubView();

                case "UISettings":
                    return new Subviews.UserInterfaceSubView();

                case "AboutApplication":
                    return new Subviews.AboutApplicationSubView();

                case "DebugSettings":
                    return new Subviews.DebugSettingsSubView();
            }
        }
    }
}
