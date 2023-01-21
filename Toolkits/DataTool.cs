using System.Collections;
using System.Text;

namespace SodaCL.Toolkits
{
	public class DataTool
	{
		public static string SplitListAndToString(ICollection targetList, string targetString)
		{
			var builder = new StringBuilder();
			foreach (var item in targetList)
			{
				if (item != null)
				{
					builder.Append(item + targetString);
					break;
				}
			}
			return builder.ToString().TrimEnd(targetString.ToCharArray());
		}
	}
}