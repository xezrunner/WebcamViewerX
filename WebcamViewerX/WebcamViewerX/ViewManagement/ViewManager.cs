using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WebcamViewerX.ViewManagement
{
    public class ViewManager
    {
        PageGatherer PageGatherer = new PageGatherer();

        /// <summary>
        /// This gives the View object its Page if it doesn't have one already, and returns the View.
        /// </summary>
        /// <param name="view">The view</param>
        /// <returns></returns>
        public View GetView(View view)
        {
            // Check if we already have that view loaded into memory.
            foreach (View _memView in _viewMemory)
            {
                // If the names match, it's a match!
                // TODO: better check? not quite sure how reliable this is..
                if (_memView.DevName == view.DevName)
                    return _memView; // Return the object from memory.
            }

            // If the View has not yet been loaded, give it its Page now.
            if (view.Page == null)
                view.Page = GetViewPage(view);

            // Add the View into the memory.
            _viewMemory.Add(view);

            // Return the newly made View.
            return view;
        }

        /// <summary>
        /// This holds the Views in memory.
        /// </summary>
        private List<View> _viewMemory = new List<View>();

        /// <summary>
        /// This returns the appropriate Page for the View specified.
        /// </summary>
        /// <param name="view">The View that needs a Page.</param>
        /// <returns></returns>
        private Page GetViewPage(View view)
        {
            Page page = PageGatherer.GetViewPage(view);

            return page;
        }
    }
}
