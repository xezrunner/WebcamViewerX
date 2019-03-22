using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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

        public async Task RequestAnimInAnimation(double? scale = 1.0)
        {
            if (!GetHasTransitionAnimations().Value)
                return;

            Storyboard board = (Storyboard)Page.FindResource("Anim_In");

            // we need a specific Duration in order for waiting to work
            TimeSpan duration;

            object setDuration = Page.TryFindResource("Anim_In_Duration");

            if (setDuration != null)
                duration = (TimeSpan)setDuration;
            else
                duration = board.Duration.TimeSpan;

            board.Begin();
            if (scale.HasValue)
                board.SetSpeedRatio(scale.Value);

            await Task.Delay(duration);
        }

        public async Task RequestAnimOutAnimation(double? scale = 1.0)
        {
            if (!GetHasTransitionAnimations().Value)
                return;

            Storyboard board = (Storyboard)Page.FindResource("Anim_Out");

            // we need a specific Duration in order for waiting to work
            TimeSpan duration;

            object setDuration = Page.TryFindResource("Anim_Out_Duration");

            if (setDuration != null)
                duration = (TimeSpan)setDuration;
            else
                duration = board.Duration.TimeSpan;

            board.Begin();
            if (scale.HasValue)
                board.SetSpeedRatio(scale.Value);

            await Task.Delay(duration);
        }

        /// <summary>
        /// Determines whether the View has its own transition animations.
        /// </summary>
        public bool? GetHasTransitionAnimations()
        {
            if (Page == null)
                return null;

            object anim_in = Page.TryFindResource("Anim_In");
            object anim_out = Page.TryFindResource("Anim_Out");

            if (anim_in != null & anim_out != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// The Page object itself.
        /// </summary>
        public Page Page;
    }
}