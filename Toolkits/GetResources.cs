using System.Windows;
using System.Windows.Media;

namespace SodaCL.Toolkits
{
	public static class GetResources

	{
		public static Brush GetBrush(string key)
		{
			return (Brush)Application.Current.Resources[key];
		}

		public static Style GetStyle(string key)
		{
			return (Style)Application.Current.Resources[key];
		}

		public static DrawingImage GetSvg(string key)
		{
			return (DrawingImage)Application.Current.Resources[key];
		}

		public static string GetText(string key)
		{
			return Application.Current.Resources[key].ToString();
		}
	}
}