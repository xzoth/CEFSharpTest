using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CEFSharpTest
{
    public partial class DownloadItemControl : UserControl
    {
        public DownloadItemControl()
        {
            InitializeComponent();
        }

        public string FileName
        {
            get
            {
                return labFileName.Text;
            }
            set
            {
                labFileName.Text = value;
            }
        }

        public ProgressBar ProgressBar
        {
            get
            {
                return this.progressBar;
            }
        }
    }
}
