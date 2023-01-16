// feature: 异步

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace SodaCL.Core.Download
{
    public class MultiDownload
    {
        #region 变量定义

        private int _threadNum;
        private long _fileSize;
        private readonly string _fileUrl;
        private string _fileName;
        private short _threadCompleteNum;
        private bool _isComplete;
        private int _wait;

        private volatile int _downloadSize;
        private readonly Thread[] _thread;
        private readonly List<string> _tempFiles = new List<string>();
        private readonly object locker = new object();

        #endregion 变量定义

        #region 属性

        public string Filename
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public long FileSize
        {
            get { return _fileSize; }
        }

        public int DownloadSize
        {
            get { return _downloadSize; }
        }

        public bool IsComplete
        {
            get { return _isComplete; }
        }

        public int ThreadNum
        {
            get { return _threadNum; }
        }

        public string SavePath { get; set; }

        #endregion 属性

        public MultiDownload(int threadNum, string fileUrl, string savePath)
        {
            this._threadNum = threadNum;
            this._thread = new Thread[threadNum];
            this._fileUrl = fileUrl;
            this.SavePath = savePath;
        }

        public void Start()
        {
            var req = (HttpWebRequest)WebRequest.Create(_fileUrl);
            var resp = (HttpWebResponse)req.GetResponse();
            _fileSize = resp.ContentLength;
            if (_fileSize / 1024 / 90 < _threadNum)
                this._threadNum = (int)(_fileSize / 1024 / 90);
            if (this._threadNum == 0)
                this._threadNum = 1;
            Console.WriteLine(Convert.ToString(_threadNum));
            this._wait = 30;
            var singleNum = (int)(_fileSize / _threadNum);
            var remainder = (int)(_fileSize % _threadNum);
            req.Abort();
            resp.Close();
            for (var i = 0; i < _threadNum; ++i)
            {
                var range = new List<int> { i * singleNum };
                if (remainder != 0 && (_threadNum - 1) == i)
                    range.Add(i * singleNum + singleNum + remainder - 1);
                else
                    range.Add(i * singleNum + singleNum - 1);
                var ran = new int[] { range[0], range[1] };
                _thread[i] = new Thread(new ParameterizedThreadStart(Download))
                {
                    Name = System.IO.Path.GetFileNameWithoutExtension(_fileUrl) + "_{0}".Replace("{0}", Convert.ToString(i + 1))
                };
                _thread[i].Start(ran);
            }
        }

        private void Download(object obj)
        {
            Stream httpFileStream = null, localFileStream = null;
            try
            {
                var ran = obj as int[];
                var tmpFileBlock = System.IO.Path.GetTempPath() + Thread.CurrentThread.Name + ".tmp";
                _tempFiles.Add(tmpFileBlock);
                var req = (HttpWebRequest)WebRequest.Create(_fileUrl);
                req.AddRange(ran[0], ran[1]);
                Console.WriteLine(Convert.ToString(ran[0]) + " " + Convert.ToString(ran[1]));

                var resp = (HttpWebResponse)req.GetResponse();
                httpFileStream = resp.GetResponseStream();
                localFileStream = new FileStream(tmpFileBlock, FileMode.Create);

                var byt = new byte[5120];
                var getByteSize = httpFileStream.Read(byt, 0, (int)byt.Length);
                while (getByteSize > 0)
                {
                    Thread.Sleep(this._wait);
                    lock (locker)
                        _downloadSize += getByteSize;
                    localFileStream.Write(byt, 0, getByteSize);
                    getByteSize = httpFileStream.Read(byt, 0, (int)byt.Length);
                }
                lock (locker)
                    _threadCompleteNum++;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                if (httpFileStream != null)
                    httpFileStream.Dispose();
                if (localFileStream != null)
                    localFileStream.Dispose();
            }
            if(_threadCompleteNum == _threadNum)
            {
                Complete();
                _isComplete = true;
            }
        }

        private void Complete()
        {
            try
            {
                Stream mergeFile = new FileStream(SavePath, FileMode.Create);
                var AddWriter = new BinaryWriter(mergeFile);
                foreach (var file in _tempFiles)
                {
                    using (var fs = new FileStream(file, FileMode.Open))
                    {
                        var Reader = new BinaryReader(fs);
                        AddWriter.Write(Reader.ReadBytes((int)fs.Length));
                        Reader.Close();
                    }

                    File.Delete(file);
                }
                AddWriter.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /*
    string httpUrl = @"https://cdn.xxx.xxx.cn/upload/image_hosting/xxxx.png";
    string saveUrl = "xxx.png";
    int threadNumber = 100;
    MultiDownload md = new MultiDownload(threadNumber, httpUrl, saveUrl);
    md.Start();
    */
}