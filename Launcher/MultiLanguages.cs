using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SodaCL.Launcher
{
    public class Languages
    {
        public static void MultiLanguages()
        {
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            //TODO:多语言切换
            string requestedCulture = @"Dictronaries\langs\zh-CN.xaml";
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}