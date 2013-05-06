using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CEFSharpTest
{
    public class RequestHandler : IRequestHandler
    {
        private IDownloadManager downloadManager;

        public RequestHandler(IDownloadManager downloadList)
        {
            downloadManager = downloadList;
        }

        public bool GetAuthCredentials(IWebBrowser browser, bool isProxy, string host, int port, string realm, string scheme, ref string username, ref string password)
        {
            return false;
        }

        public bool GetDownloadHandler(IWebBrowser browser, string mimeType, string fileName, long contentLength, ref IDownloadHandler handler)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = fileName;
            string strExt = Path.GetExtension(fileName);
            dialog.Filter = string.Format("{0} 文件(*{0})|*{0}", strExt);
            dialog.RestoreDirectory = true;
            dialog.OverwritePrompt = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.AutoUpgradeEnabled = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dialog.FileName;
                if (!string.IsNullOrEmpty(strFilePath))
                {
                    //string strFilePath = Path.Combine(strFolderPath, fileName);                   

                    var downloadItem = new DownloadItem()
                    {
                        DownloadID = Guid.NewGuid(),
                        SourceFileName = fileName,
                        ContentLength = contentLength,
                        MineType = mimeType,
                        SavePath = strFilePath,
                        SaveFileName = Path.GetFileName(strFilePath),
                        StartTime = DateTime.Now
                    };
                    downloadManager.Add(downloadItem);

                    DownloadHandler downloadHandler = new DownloadHandler(downloadItem);
                    downloadHandler.OnDownloadComplete += downloadHandler_OnDownloadComplete;
                    downloadHandler.OnDownloadNotify += downloadHandler_OnDownloadNotify;

                    handler = downloadHandler;





                    (downloadManager as FormDownloadManger).Show();
                    downloadManager.UpdateView();
                    
                    return true;
                }
            }

            return false;
        }

        void downloadHandler_OnDownloadNotify(DownloadNotifyEventArg e)
        {
            var item = downloadManager.GetByID(e.DownloadID);
            item.CurrLength += e.DataLength;
            
            e.IsCancel = item.IsCancel;

            downloadManager.UpdateView();
        }

        void downloadHandler_OnDownloadComplete(Guid id)
        {
            var item = downloadManager.GetByID(id);
            item.CompleteTime = DateTime.Now;
            item.CurrLength = item.ContentLength;

            downloadManager.Complete(item);
        }

        public bool OnBeforeBrowse(IWebBrowser browser, IRequest request, NavigationType naigationvType, bool isRedirect)
        {
            return false;
        }

        public bool OnBeforeResourceLoad(IWebBrowser browser, IRequestResponse requestResponse)
        {
            return false;
        }

        public void OnResourceResponse(IWebBrowser browser, string url, int status, string statusText, string mimeType, System.Net.WebHeaderCollection headers)
        {
            
        }
    }
}
