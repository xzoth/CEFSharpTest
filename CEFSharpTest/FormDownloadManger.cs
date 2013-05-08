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
            bool isOverFlow = (item.ContentLength > (long)int.MaxValue);

            if (isOverFlow)
            {
                //计算单位刻度
                long ProgressBarSpan = item.ContentLength / int.MaxValue;
                itemControl.ProgressBar.Maximum = int.MaxValue;
                itemControl.ProgressBar.Value = (int)(item.CurrLength / ProgressBarSpan);
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
                var itemControl = itemPanel.Controls.Find(item.DownloadID.ToString(), false)[0] as DownloadItemControl;

                UpdateProgressBar(item, itemControl);
            }
        }

        public void Complete(DownloadItem item)
        {
            //TODO: play a sound

            item.IsComplete = true;
        }

        public void UpdateView()
        {
            itemPanel.Controls.Clear();

            foreach (var item in downloadList)
            {
                var itemControl = new DownloadItemControl();
                itemControl.FileName = item.SourceFileName;

                bool isOverFlow = (item.ContentLength > (long)int.MaxValue);

                if (isOverFlow)
                {
                    //计算单位刻度
                    long ProgressBarSpan = item.ContentLength / int.MaxValue;
                    itemControl.ProgressBar.Maximum = int.MaxValue;
                    itemControl.ProgressBar.Value = (int)(item.CurrLength / ProgressBarSpan);
                }
                else
                {
                    itemControl.ProgressBar.Maximum = (int)item.ContentLength;
                    itemControl.ProgressBar.Value = (int)item.CurrLength;
                }

                //itemControl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                //itemControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left)))) ;

                itemPanel.Controls.Add(itemControl);
            }
        }

        private void FormDownloadManger_Load(object sender, EventArgs e)
        {
            //MockList.Add(new DownloadItem()
            //{
            //    DownloadID = Guid.NewGuid(),
            //    FileName = "windows8 x64.iso",
            //    ContentLength = 409600,
            //    CurrLength = 10240
            //});

            //MockList.Add(new DownloadItem()
            //{
            //    DownloadID = Guid.NewGuid(),
            //    FileName = "office 2013 toolkit.rar",
            //    ContentLength = 2560,
            //    CurrLength = 1299
            //});

            //UpdateView();
        }

        public IList<DownloadItem> MockList = new List<DownloadItem>();

        private void FormDownloadManger_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
