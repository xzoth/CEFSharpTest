using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CEFSharpTest
{
    public partial class FormDownloadManger : Form, IDownloadManager
    {
        protected static IList<DownloadItem> downloadList;

        static FormDownloadManger()
        {
            downloadList = new List<DownloadItem>();
        }

        public FormDownloadManger()
        {
            InitializeComponent();
        }

        public IList<DownloadItem> GetList()
        {
            return downloadList;
        }

        public DownloadItem GetByID(Guid id)
        {
            return downloadList.FirstOrDefault(item => item.DownloadID == id);
        }

        public bool Cancel(DownloadItem item)
        {
            item.IsCancel = true;

            return true;
        }

        public bool Add(DownloadItem item)
        {
            downloadList.Add(item);

            item.PropertyChanged += item_PropertyChanged;

            var itemControl = new DownloadItemControl();
            itemControl.Name = item.DownloadID.ToString();
            itemControl.FileName = item.SaveFileName;

            UpdateProgressBar(item, itemControl);

            itemPanel.Controls.Add(itemControl);

            return true;
        }

        private static void UpdateProgressBar(DownloadItem item, DownloadItemControl itemControl)
        {
            //文件大小溢出
            if (item.ContentLength > (long)int.MaxValue)
            {
                //计算单位刻度
                long ProgressBarSpan = item.ContentLength / int.MaxValue;
                itemControl.ProgressBar.Maximum = int.MaxValue;
                itemControl.ProgressBar.Value = (int)(item.CurrLength / ProgressBarSpan);
            }
            else if (item.ContentLength == -1)//文件大小无法获得
            {
                itemControl.ProgressBar.Style = ProgressBarStyle.Marquee;
                //itemControl.ProgressBar.MarqueeAnimationSpeed = 1000;
            }
            else
            {
                itemControl.ProgressBar.Maximum = (int)item.ContentLength;
                itemControl.ProgressBar.Value = (int)item.CurrLength;
            }
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == DownloadItem.CONST_PROPERTY_CURRLENGTH)
            {
                var item = sender as DownloadItem;
                var itemControl = GetControlByID(item.DownloadID);

                UpdateProgressBar(item, itemControl);
            }
        }

        DownloadItemControl GetControlByID(Guid downloadID)
        {
            return (itemPanel.Controls.Find(downloadID.ToString(), false)[0] as DownloadItemControl);
        }

        public void Complete(DownloadItem item)
        {
            //TODO: play a sound

            item.IsComplete = true;
            var itemControl = GetControlByID(item.DownloadID);

            itemControl.ProgressBar.Visible = false;
        }

        private void FormDownloadManger_Load(object sender, EventArgs e)
        {
        }

        private void FormDownloadManger_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
