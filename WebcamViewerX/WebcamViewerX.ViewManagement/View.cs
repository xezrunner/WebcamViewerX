using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WebcamViewerX.Hooks;

namespace WebcamViewerX.ViewManagement
{
    public enum ViewTitlebarBehavior
    {
        ExtendsUnderTitlebar,
        ExtendsAboveTitlebar,
        ExtendsBeyondWindow,
        NoTitlebar
    }

    public class View
    {
        /// <summary>
        /// The developer name for the View.
        /// </summary>
        public string DevName;

        /// <summary>
        /// The title of the View used in UI.
        /// </summary>
        public string Title;

        /// <summary>
        /// The View's titlebar behavior.
        /// </summary>
        public ViewTitlebarBehavior TitlebarBehavior;

        /// <summary>
        /// Determines whether the View has its own transition animations.
        /// </summary>
        public bool HasTransitionAnimations()
        {
            object anim_in = Page.TryFindResource("Anim_In");
            object anim_out = Page.TryFindResource("Anim_Out");

            if (anim_in != null & anim_out != null)
                return true;
            else
                return false;
        }

        public MainWindowHooks Hooks
        {
            get { return (MainWindowHooks)Page.Tag; }
        }

        /// <summary>
        /// The Page object itself.
        /// </summary>
        public Page Page;
    }
}
