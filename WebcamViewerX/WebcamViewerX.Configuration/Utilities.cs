using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebcamViewerX.Configuration
{
    public class Utilities
    {
        /// <summary>
        /// AppDirectory\Configuration\...
        /// </summary>
        public string GetPathForConfigurationFile(string file, ConfigCategories category = ConfigCategories.Application)
        {
            // Configuration directory path
            string configdirname;

            switch (category)
            {
                default:
                    configdirname = "Configuration"; break;

                case ConfigCategories.Application:
                    configdirname = "Configuration"; break;
                case ConfigCategories.User:
                    configdirname = "UserConfiguration"; break;
            }

            string configDir = String.Format(AppDomain.CurrentDomain.BaseDirectory + @"{0}\", configdirname);

            // If the configuration directory doesn't exist, create it.
            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);

            return configDir + file;
        }

        public enum ConfigCategories
        {
            Application,
            User
        }
    }
}
