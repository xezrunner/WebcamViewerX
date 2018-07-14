using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebcamViewerX.Hooks
{
    public class MainWindowHooks
    {
        public TitlebarHooks TitlebarHooks;

        // TODO TODO TODO:
        // Request events somehow from within other Views..
        // There's a hacky workaround for the titlebar buttons, but the other events require triggering from within the other View only, the MainWindow isn't the one triggering these events..

        public MainWindowHooks()
        {
            TitlebarHooks = new TitlebarHooks();
        }
    }
}
