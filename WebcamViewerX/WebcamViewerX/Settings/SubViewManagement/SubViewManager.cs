using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WebcamViewerX.Settings
{
    class SubViewManager
    {
        SubViewPageGatherer PageGatherer = new SubViewPageGatherer();

        public SubView GetSubView(SubView subview)
        {
            foreach (SubView _memsubView in _subviewMemory)
            {
                if (_memsubView.DevName == subview.DevName)
                    return _memsubView;
            }

            if (subview.Page == null)
                subview.Page = GetSubViewPage(subview);

            _subviewMemory.Add(subview);

            return subview;
        }

        private List<SubView> _subviewMemory = new List<SubView>();

        Page GetSubViewPage(SubView subview)
        {
            Page page = PageGatherer.GetSubViewPage(subview);
            return page;
        }
    }
}
