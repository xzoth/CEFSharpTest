using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEFSharpTest
{
    public class DownloadNotifyEventArg : EventArgs
    {
        private Guid id;
        public Guid DownloadID
        {
            get
            {
                return id;
            }
        }

        public int DataLength { get; set; }

        private bool isCancel;
        public bool IsCancel
        {
            get
            {
                return isCancel;
            }
            set
            {
                isCancel = value;
            }
        }

        public DownloadNotifyEventArg(Guid downloadID)
        {
            id = downloadID;
        }
    }
}
