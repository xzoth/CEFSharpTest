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
            UpdateView();

            return true;
        }

        public bool Add(DownloadItem item)
        {
            downloadList.Add(item);

            UpdateView();

            return true;
        }

        public void Complete(DownloadItem item)
        {
            //TODO: play a sound

            UpdateView();
        }

        public void UpdateView()
        {
            itemPanel.Controls.Clear();

            foreach (var item in downloadList)
            {
                var itemControl = new DownloadItemControl();
                itemControl.FileName = item.SourceFileName;
                unsafe
                {
                    if (item.ContentLength > (long)int.MaxValue)
                    {
                        //此处溢出
                        itemControl.ProgressBar.Maximum = (int)item.ContentLength;
                    }
                    itemControl.ProgressBar.Value = (int)item.CurrLength;
                    //itemControl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    //itemControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left)))) ;
                }

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
    }
}
