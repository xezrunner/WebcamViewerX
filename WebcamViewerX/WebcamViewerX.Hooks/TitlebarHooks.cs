using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebcamViewerX.Hooks
{
    public class TitlebarHooks
    {
        public event RoutedEventHandler MenuButtonClick;
        public event RoutedEventHandler BackButtonClick;

        public event EventHandler RequestTitlebarAppTitleChange;
        public event EventHandler RequestTitlebarAppTitleVisibilityChange;
        public event EventHandler RequestTitlebarColorsChange;
        public event EventHandler RequestTitlebarButtonsChange;
        public event EventHandler RequestTitlebarSizeChange;

        // TODO:
        // Hacks! Hacks! Hacks!
        public void InvokeMenuButtonClick()
        {
            MenuButtonClick?.Invoke(null, null);
        }

        public void InvokeBackButtonClick()
        {
            BackButtonClick?.Invoke(null, null);
        }
    }
}
