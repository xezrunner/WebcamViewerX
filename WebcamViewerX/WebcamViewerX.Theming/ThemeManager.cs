using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebcamViewerX.Theming
{
    public class ThemeManager
    {
        public Config Config = Config.Default;

        public enum Theme
        {
            Light,
            Dark
        }

        public ResourceDictionary Dictionary;

        public ThemeManager(ResourceDictionary _dict)
        {
            Dictionary = _dict;

            // change theme from config
            RequestThemeChange(GetThemeFromString(Config.theme));

            Config.Default.PropertyChanged += Config_PropertyChanged;
        }

        /// <summary>
        /// Returns a Theme property from a string, if valid argument is given.
        /// </summary>
        /// <param name="arg">The string to try and get the Theme name from.</param>
        public Theme GetThemeFromString(string arg)
        {
            switch (arg)
            {
                default:
                    return Theme.Light;

                case "Light":
                    return Theme.Light;
                case "Dark":
                    return Theme.Dark;
            }
        }

        /// <summary>
        /// This changes the Dictionary to the said theme one.
        /// NOTE: use Config_SetTheme() to change the theme throught the entire app
        /// </summary>
        /// <param name="theme">The theme that we'll use to get its dictionary and swap it in.</param>
        public void RequestThemeChange(Theme theme)
        {
            // clear merged dictionaries of dictionary
            Dictionary.MergedDictionaries.Clear();
            // create new theme dictionary
            ResourceDictionary newMergedDictionary = new ResourceDictionary() { Source = new Uri(String.Format("pack://application:,,,/WebcamViewerX.Theming;component/Themes/Theme{0}.xaml", theme.ToString())) };
            // add new theme dictionary
            Dictionary.MergedDictionaries.Add(newMergedDictionary);
        }

        /// <summary>
        /// This listens to the theme property changing in Config.
        /// If it changes, loads in the appropriate theme.
        /// </summary>
        private void Config_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "theme")
                RequestThemeChange(GetThemeFromString(Config.theme));
        }

        /// <summary>
        /// Set the theme globally.
        /// </summary>
        /// <param name="theme">The theme to set.</param>
        /// <param name="apply">Controls whether to apply the theme right away.</param>
        /// <param name="save">Controls whether to save the theme change permanently.</param>
        public void Config_SetTheme(Theme theme, bool apply = true, bool save = true)
        {
            // TODO: might need cross-project QuickConfig compatibility with this.
            // XesignPhotos QC Editor already has color-picker and changing capability.

            // set the config value
            Config.Default.theme = theme.ToString();
            if (save)
                Config.Save();

            // apply the theme
            if (apply)
                RequestThemeChange(theme);
        }
    }
}
