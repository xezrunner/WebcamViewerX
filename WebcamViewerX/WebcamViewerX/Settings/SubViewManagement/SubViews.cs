using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebcamViewerX.Settings
{
    public class SubViews
    {
        public SubView GetSubViewFromID(int id)
        {
            switch (id)
            {
                default:
                    return null;

                case 0:
                    return WebcamEditorSubView;
                case 1:
                    return HomeSettingsSubView;
                case 2:
                    return UserInterfaceSubView;
                case 3:
                    return AboutSubView;
                case 4:
                    return DebugSettingsSubView;
            }
        }

        #region Webcams
        public SubView WebcamEditorSubView = new SubView()
        {
            Title = "Webcam editor",
            DevName = "WebcamEditor"
        };

        public SubView HomeSettingsSubView = new SubView()
        {
            Title = "Viewer settings",
            DevName = "HomeSettings"
        };
        #endregion

        #region Personalization
        public SubView UserInterfaceSubView = new SubView()
        {
            Title = "User interface",
            DevName = "UISettings"
        };
        #endregion

        #region About & updates
        public SubView AboutSubView = new SubView()
        {
            Title = "About application",
            DevName = "AboutApplication"
        };
        #endregion

        #region Developer options
        public SubView DebugSettingsSubView = new SubView()
        {
            Title = "Debug settings",
            DevName = "DebugSettings"
        };
        #endregion
    }
}
