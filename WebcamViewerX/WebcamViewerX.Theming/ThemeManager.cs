using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebcamViewerX.Theming
{
    // TODO: ThemeManager: make improvements, including Accent colors.
    // Right now, this is pretty much copy-paste from XesignPhotos.ThemeManager, and that app had/has a single accent color to worry about.
    // We're to be keeping all of the main UI from Webcam Viewer "9" (overviewy), but improving it *a lot*.
    // That means we're keeping the accent color choices, and perhaps we're even adding a third Black theme, as the new theme engine actually supports more than 2 themes already, theoretically.
    // I've had an idea for event-specific themes, such as for winter, Christmas etc... aswell..
    public class ThemeManager
    {
        public Config Config = Config.Default;

        public enum Theme
        {
            Light,
            Dark
        }

        public ResourceDictionary Dictionary;

        XeZrunner.UI.Theming.ThemeManager XZ_ThemeManager = new XeZrunner.UI.Theming.ThemeManager(null);

        public ThemeManager(ResourceDictionary _dict)
        {
            Dictionary = _dict;

            // change theme from config
            RequestThemeChange(GetThemeFromString(Config.theme));

            Config.Default.PropertyChanged += Config_PropertyChanged;
        }

        public bool ListenToConfigChange { get; set; } = true;

        public event EventHandler ThemeChangeRequested;

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
            ThemeChangeRequested?.Invoke(this, null);

            // clear merged dictionaries of dictionary
            Dictionary.MergedDictionaries.Clear();
            // create new theme dictionary
            ResourceDictionary newMergedDictionary = new ResourceDictionary() { Source = new Uri(String.Format("pack://application:,,,/WebcamViewerX.Theming;component/Themes/Theme{0}.xaml", theme.ToString())) };
            // add new theme dictionary
            Dictionary.MergedDictionaries.Add(newMergedDictionary);

            XZ_ThemeManager.Config_SetTheme(XZ_ThemeManager.GetThemeFromString(Config.theme));
        }

        /// <summary>
        /// This listens to the theme property changing in Config.
        /// If it changes, loads in the appropriate theme.
        /// </summary>
        private void Config_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (ListenToConfigChange & e.PropertyName == "theme")
                RequestThemeChange(GetThemeFromString(Config.theme));
        }

        /// <summary>
        /// Set the theme globally.
        /// </summary>
        /// <param name="theme">The theme to set.</param>
        /// <param name="apply">Controls whether to apply the theme right away.</param>
        /// <param name="save">Controls whether to save the theme change permanently.</param>
        public void Config_SetTheme(Theme theme, bool save = true)
        {
            // TODO: might need cross-project QuickConfig compatibility with this.
            // XesignPhotos QC Editor already has color-picker and changing capability.

            // set the config value. we're already listening to config-changing so no need to apply theme.
            if (save)
            {
                Config.Default.theme = theme.ToString();
                Config.Save();
            }

            // apply the theme if not saving
            if (!save)
                RequestThemeChange(theme);
        }
    }
}
