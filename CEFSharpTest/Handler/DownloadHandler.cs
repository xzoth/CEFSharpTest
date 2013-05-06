using CefSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CEFSharpTest
{
    public class DownloadHandler : IDownloadHandler
    {
        private Guid downloadID;
        private string strFilePath;
        private Stream stream;

        private readonly object eventLock = new object();

        private DownloadNotifyDelegate downloadNotifyEventHandler;
        public event DownloadNotifyDelegate OnDownloadNotify
        {
            add
            {
                lock (eventLock)
                {
                    downloadNotifyEventHandler += value;
                }
            }
            remove
            {
                lock (eventLock)
                {
                    downloadNotifyEventHandler -= value;
                }
            }
        }

        private DownloadCompleteDelegate downloadCompleteEventHandler;
        public event DownloadCompleteDelegate OnDownloadComplete
        {
            add
            {
                lock (eventLock)
                {
                    downloadCompleteEventHandler += value;
                }
            }
            remove
            {
                lock (eventLock)
                {
                    downloadCompleteEventHandler -= value;
                }
            }
        }

        public DownloadHandler(DownloadItem item)
        {
            strFilePath = item.SavePath;
            downloadID = item.DownloadID;

            stream = File.Create(strFilePath);
        }

        public bool ReceivedData(byte[] data)
        {
            int dataLength = data.GetLength(0);            

            var notifyEventArg = new DownloadNotifyEventArg(downloadID);
            notifyEventArg.DataLength = dataLength;
            downloadNotifyEventHandler(notifyEventArg);

            if (notifyEventArg.IsCancel)
            {
                CloseDownload();
                return false;
            }
            else
            {
                stream.Write(data, 0, dataLength);                
            }

            return true;
        }

        public void Complete()
        {
            CloseDownload();

            downloadCompleteEventHandler(downloadID);
        }

        private void CloseDownload()
        {
            stream.Flush();
            stream.Close();
            stream.Dispose();
            stream = null;
        }
    }
}
