using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEFSharpTest
{
    public delegate void DownloadCompleteDelegate(Guid id);
    public delegate void DownloadNotifyDelegate(DownloadNotifyEventArg e);
}
