using System.Windows;
using System.Windows.Media;

namespace SodaCL.Toolkits
{
    public static class GetResources
    {
        public static string GetI18NText(string key)
        {
            var targetString = Application.Current.Resources[key].ToString();
            return targetString;
        }

        public static Brush GetBrush(string key)
        {
            var targetBrush = (Brush)Application.Current.Resources[key];
            return targetBrush;
        }

        public static Style GetStyle(string key)
        {
            var targetStyle = (Style)Application.Current.Resources[key];
            return targetStyle;
        }
        public static DrawingImage GetSvg(string key)
        {
            var targetSvg = (DrawingImage)Application.Current.Resources[key];
            return targetSvg;
        }

    }
}