using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEFSharpTest
{
    public class DownloadItem
    {
        public DateTime StartTime { get; set; }
        public DateTime CompleteTime { get; set; }
        public string MineType { get; set; }
        public string SourceFileName { get; set; }
        public long ContentLength { get; set; }
        public long CurrLength { get; set; }
        public long Percent
        {
            get
            {
                if (ContentLength > 0)
                {
                    return CurrLength / ContentLength;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string SavePath { get; set; }
        public string SaveFileName { get; set; }
        public Guid DownloadID { get; set; }
        public bool IsCancel { get; set; }
    }
}
