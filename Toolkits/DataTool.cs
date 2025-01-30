using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Media;

namespace SodaCL.Toolkits {

	public class DataTool {

		/// <summary>
		/// 释放内嵌资源至指定位置
		/// </summary>
		/// <param name="resource">嵌入的资源，此参数写作：命名空间.文件夹名.文件名.扩展名</param>
		/// <param name="path">释放到位置</param>
		public static void ExtractFile(string resource, string path) {
			try {
				var assembly = Assembly.GetExecutingAssembly();
				var res = assembly.GetManifestResourceStream(resource);
				var input = new BufferedStream(res);
				var output = new FileStream(path, FileMode.Create);
				var data = new byte[1024];
				int lengthEachRead;
				while ((lengthEachRead = input.Read(data, 0, data.Length)) > 0) {
					output.Write(data, 0, lengthEachRead);
				}
				output.Flush();
				output.Close();
			}
			catch (Exception ex) {
				Logger.Log(false, Logger.ModuleList.IO, Logger.LogInfo.Error, "释放资源文件失败。", ex);
				throw;
			}
		}

		/// <summary>

		/// 将 <see cref="SolidColorBrush"/> 对象 转换为 <see cref="Color"/> 对象
		/// </summary>
		/// <param name="targetBrush">源对象</param>
		/// <returns></returns>
		public static Color BrushToColor(Brush targetBrush) {
			return ((SolidColorBrush)targetBrush).Color;
		}

		/// <summary>
		/// 为目录后增加 "//"
		/// </summary>
		/// <param name="rawDir">原目录</param>
		/// <returns></returns>
		public static string DirConverter(string rawDir) {
			if (rawDir.EndsWith("\\"))
				return rawDir;
			else if (string.IsNullOrEmpty(rawDir) || string.IsNullOrWhiteSpace(rawDir))
				throw new System.Exception("目录格式非法");
			else
				return rawDir + "\\";
		}

		/// <summary>
		/// 将路径中 "/" 改为 "\\"
		/// </summary>
		/// <param name="rawPath">原路径</param>
		/// <returns></returns>
		public static string PathConverter(string rawPath) {
			if (rawPath.EndsWith("\\"))
				throw new System.Exception("文件路径非法");
			else
				return rawPath.Replace("/", "\\");
		}

		/// <summary>
		/// 使用指定的字符将支持 <see cref="ICollection"/> 接口的对象中的每一项拼接成一个字符串
		/// </summary>
		/// <param name="targetList">源对象</param>
		/// <param name="targetString">目标字符</param>
		/// <returns><see langword="string"/> 拼接完成的对象</returns>
		public static string SplitListAndToString(ICollection targetList, string targetString) {
			var builder = new StringBuilder();
			foreach (var item in targetList) {
				if (item != null) {
					builder.Append(item + targetString);
					break;
				}
			}
			return builder.ToString().TrimEnd(targetString.ToCharArray());
		}
	}
}