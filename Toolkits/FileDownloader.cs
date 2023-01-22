using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace SodaCL.Toolkits
{
	public class FileDownloader
	{
		#region 字段

		private object _locker = new object();
		private short _threadCompleteNum;
		private List<string> _tmpFiles = new(); //线程完成数量
		private Thread[] Threads;//线程数组

		#endregion 字段

		#region 事件

		public event EventHandler<long> DownloaderProgressChanged;

		public event EventHandler DownloaderProgressFinished;

		#endregion 事件

		#region 属性

		public int DownloadSize { get; private set; }   //实时的

		public long FileSize { get; private set; }

		public string FileUrl { get; private set; }
		public bool IsComplete { get; private set; }
		public string SavePath { get; private set; }
		public int ThreadsNum { get; private set; }

		#endregion 属性

		/// <summary>
		/// 单文件多线程下载器
		/// </summary>
		/// <param name="fileUrl">文件网址</param>
		/// <param name="savePath">保存的路径</param>
		/// <param name="threadsNum">线程数 (可选）默认32)</param>
		public FileDownloader(string fileUrl, string savePath, int threadsNum = 32)
		{
			this.ThreadsNum = threadsNum;
			this.Threads = new Thread[threadsNum];
			this.FileUrl = fileUrl;
			this.SavePath = savePath;
		}

		public void Start()
		{
			using HttpClient hc = new();
			var response = hc.GetAsync(this.FileUrl).Result;
			if (response.IsSuccessStatusCode)
			{
				var content = response.Content;
				var contentStream = content.ReadAsStream();
				this.FileSize = contentStream.Length;
			}
			else
			{
				throw new FileNotFoundException();
			}
			var singleNum = (int)(FileSize / ThreadsNum);//平均分配
			var remainder = (int)(FileSize % ThreadsNum);//获取剩余的
			for (var i = 0; i < ThreadsNum; i++)
			{
				var range = new List<int>
				{
					i * singleNum
				};
				if (remainder != 0 && (ThreadsNum - 1) == i) //剩余的交给最后一个线程
					range.Add(i * singleNum + singleNum + remainder - 1);
				else
					range.Add(i * singleNum + singleNum - 1);
				//下载指定位置的数据
				var ran = new int[] { range[0], range[1] };
				Threads[i] = new Thread(new ParameterizedThreadStart(Download));
				Threads[i].Name = Path.GetFileNameWithoutExtension(FileUrl) + "_{0}".Replace("{0}", Convert.ToString(i + 1));
				Threads[i].Start(ran);
			}
		}

		/// <summary>
		/// 下载完成后合并文件块
		/// </summary>
		private void Complete()
		{
			var mergeFile = new FileStream(SavePath, FileMode.Create);
			var AddWriter = new BinaryWriter(mergeFile);
			foreach (var file in _tmpFiles)
			{
				using (var fs = new FileStream(file, FileMode.Open))
				{
					var TempReader = new BinaryReader(fs);
					AddWriter.Write(TempReader.ReadBytes((int)fs.Length));
					TempReader.Close();
				}
				File.Delete(file);
			}
			AddWriter.Close();
		}

		private void Download(object obj)
		{
			Stream httpFileStream = null, localFileStram = null;
			try
			{
				var ran = obj as int[];
				var tmpFileBlock = Path.GetTempPath() + Thread.CurrentThread.Name + ".tmp";
				_tmpFiles.Add(tmpFileBlock);
				using HttpClient hc = new();
				hc.DefaultRequestHeaders.Range = new RangeHeaderValue(ran[0], ran[1]);
				var httpresponse = hc.GetAsync(FileUrl).Result;
				httpFileStream = httpresponse.Content.ReadAsStream();
				localFileStram = new FileStream(tmpFileBlock, FileMode.Create);
				var by = new byte[5000];
				var getByteSize = httpFileStream.Read(by, 0, (int)by.Length); //Read方法将返回读入by变量中的总字节数
				while (getByteSize > 0)
				{
					Thread.Sleep(20);
					lock (_locker)
					{
						DownloadSize += getByteSize;
						DownloaderProgressChanged?.Invoke(this, DownloadSize / FileSize);
					}
					localFileStram.Write(by, 0, getByteSize);
					getByteSize = httpFileStream.Read(by, 0, (int)by.Length);
				}
				lock (_locker) _threadCompleteNum++;
			}
			catch (Exception ex)
			{
				Logger.Log(true, Logger.ModuleList.IO, Logger.LogInfo.Error, ex: ex);
			}
			finally
			{
				if (httpFileStream != null) httpFileStream.Dispose();
				if (localFileStram != null) localFileStram.Dispose();
			}
			if (_threadCompleteNum == ThreadsNum)
			{
				Complete();
				DownloaderProgressFinished?.Invoke(this, null);
				IsComplete = true;
			}
		}
	}
}