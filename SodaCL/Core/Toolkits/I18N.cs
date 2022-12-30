using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SodaCL.Core.Toolkits
{
    public static class I18N
    {
        public static string GetI18NText(string key)
        {
            var targetString = (string)Application.Current.Resources.FindName(key);
            return targetString;
        }
    }
}
