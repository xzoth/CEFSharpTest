using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CEFSharpTest
{
    public class DownloadItem : INotifyPropertyChanged
    {
        public DateTime StartTime { get; set; }
        public DateTime CompleteTime { get; set; }
        public string MineType { get; set; }
        public string SourceFileName { get; set; }
        public long ContentLength { get; set; }

        public const string CONST_PROPERTY_CURRLENGTH = "CurrLength";
        private long currLength;
        public long CurrLength 
        {
            get
            {
                return currLength;
            }
            set
            {
                if (currLength != value)
                {
                    currLength = value;

                    OnPropertyChanged(CONST_PROPERTY_CURRLENGTH);
                }
            }
        }

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
        public bool IsComplete { get; set; }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
