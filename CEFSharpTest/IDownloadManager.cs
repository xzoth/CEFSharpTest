using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEFSharpTest
{
    public interface IDownloadManager
    {
        IList<DownloadItem> GetList();
        DownloadItem GetByID(Guid id);
        bool Cancel(DownloadItem item);
        bool Add(DownloadItem item);
        void Complete(DownloadItem item);
        void UpdateView();
    }
}
