using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CEFSharpTest
{
    public partial class Form1 : Form
    {
        protected WebView webView = null;
        private string strURL = @"https://html5test.com";
        private static FormDownloadManger frmDownloadList = new FormDownloadManger();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (webView == null)
            {
                var setting = new BrowserSettings()
                {
                    HistoryDisabled = true,
                    MinimumFontSize = 12
                };

                webView = new WebView(strURL, setting);
                webView.Dock = DockStyle.Fill;
                this.Controls.Add(webView);

                txtURL.Text = strURL;
            }

            webView.RequestHandler = new RequestHandler(frmDownloadList);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            webView.Load(txtURL.Text.Trim());
        }
    }
}
