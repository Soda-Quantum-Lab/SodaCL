using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SodaCL.Launcher
{
    public class Languages
    {
        public static void MultiLanguages()
        {
            var dictionaryList = new List<ResourceDictionary>();
            foreach (var dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            //TODO:多语言切换
            var requestedCulture = @"Dictronaries\langs\zh-CN.xaml";
            var resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}