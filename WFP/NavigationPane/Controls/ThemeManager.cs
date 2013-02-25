#region Copyright and License Information
// NavigationPane Control
// http://NavigationPane.codeplex.com/
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://NavigationPane.codeplex.com/license
#endregion

using System.Windows;
using System.Windows.Threading;
using System;

namespace Stema.Controls
{
    public enum NavigationPaneTheme
    {
        WindowsLive,
        Office2007Blue,
        Office2007Silver,
        Office2007Black,
        Office2010Blue,
        Office2010Silver,
        Office2010Black,
        MetroBlue
    }

    public static class ThemeManager
    {
        private static Uri GetThemeUri(NavigationPaneTheme theme)
        {
            string s = "pack://application:,,,/NavigationPane;component/Themes/NavigationPane/";
            switch (theme)
            {
                case NavigationPaneTheme.WindowsLive:
                    s += "WindowsLive/Live.xaml";
                    break;
                case NavigationPaneTheme.Office2007Blue:
                    s += "Office2007/Blue.xaml";
                    break;
                case NavigationPaneTheme.Office2007Silver:
                    s += "Office2007/Silver.xaml";
                    break;
                case NavigationPaneTheme.Office2007Black:
                    s += "Office2007/Black.xaml";
                    break;
                case NavigationPaneTheme.Office2010Blue:
                    s += "Office2010/Blue.xaml";
                    break;
                case NavigationPaneTheme.Office2010Silver:
                    s += "Office2010/Silver.xaml";
                    break;
                case NavigationPaneTheme.Office2010Black:
                    s += "Office2010/Black.xaml";
                    break;
                case NavigationPaneTheme.MetroBlue:
                    s += "Metro/Blue.xaml";
                    break;
            }
            return new Uri(s);
        }

        private delegate void ChangeTheme(NavigationPaneTheme theme, ResourceDictionary destination, bool reinitDestination);

        /// <summary>
        /// Loads the theme specified and merge it to the Application Resources Dictionary
        /// This would reinit the Application ResourceDictionary
        /// </summary>
        /// <param name="theme">The Theme to be loaded</param>
        public static void SetActiveTheme(NavigationPaneTheme theme)
        {
            SetActiveTheme(theme, Application.Current.Resources, true);
        }

        /// <summary>
        /// Loads the theme specified and merge it to the specified ResourceDictionary
        /// This would reinit the destination ResourceDictionary
        /// </summary>
        /// <param name="theme">The Theme to be loaded</param>
        /// <param name="destination">The ReourceDictionary where the theme dhata should be merged</param>
        public static void SetActiveTheme(NavigationPaneTheme theme, ResourceDictionary destination)
        {
            SetActiveTheme(theme, destination, true);
        }

        /// <summary>
        /// Loads the theme specified and merge it to the specified ResourceDictionary 
        /// specifing if the destination ResourceDictionary should be reinitialized 
        /// </summary>
        /// <param name="theme">The Theme to be loaded</param>
        /// <param name="destination">The ReourceDictionary where the theme dhata should be merged</param>
        /// <param name="reinitDestination">true to reinitialize the destination ResourceDictionary</param>
        public static void SetActiveTheme(NavigationPaneTheme theme, ResourceDictionary destination, bool reinitDestination)
        {
            Application.Current.Dispatcher.BeginInvoke(
             (ChangeTheme)((t, d, reinit) =>
             {
                 bool merge = true;
                 Uri dictSource = GetThemeUri(theme);
                 foreach (ResourceDictionary dict in d.MergedDictionaries)
                     if (dict.Source != null && dict.Source.Equals(dictSource)) // microsoft blend inject resource without source... so we check it !
                     {
                         merge = false;
                         break;
                     }

                 if (merge)
                 {
                     d.BeginInit();
                     for (int j = d.MergedDictionaries.Count - 1; j >= 0; j--)
                     {
                         ComponentResourceKey NameRes = new ComponentResourceKey(typeof(NavigationPane), "ActiveTheme");
                         if (d.MergedDictionaries[j].Contains(NameRes))
                         {
                             d.MergedDictionaries.Remove(d.MergedDictionaries[j]);
                         }
                     }
                     d.MergedDictionaries.Add(new ResourceDictionary { Source = dictSource });
                     d.EndInit();
                 }
             }
             ), DispatcherPriority.ApplicationIdle, theme, destination, reinitDestination);
        }

        /// <summary>
        /// Check if there's a theme presennt and returns the theme name if founded
        /// </summary>
        /// <returns>The Active Theme Name</returns>
        public static object GetActiveTheme()
        {
            return Application.Current.TryFindResource(new ComponentResourceKey(typeof(NavigationPane), "ActiveTheme"));
        }

        internal static void CheckThemePresent()
        {
            if (GetActiveTheme() == null)
                SetActiveTheme(NavigationPaneTheme.Office2010Silver);
        }
    }
}
