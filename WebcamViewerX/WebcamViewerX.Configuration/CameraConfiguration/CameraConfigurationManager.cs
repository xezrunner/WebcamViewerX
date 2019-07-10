using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebcamViewerX.Engine;

namespace WebcamViewerX.Configuration.CameraConfiguration
{
    public class CameraConfigurationManager
    {
        Utilities Utilities = new Utilities();

        public List<Camera> GetUserCameras()
        {
            string file = File.ReadAllText(Utilities.GetPathForConfigurationFile("CameraConfig.json", Utilities.ConfigCategories.User));
            return JsonConvert.DeserializeObject<List<Camera>>(file);
        }

        // TODO: Download default camera config from GitHub Repo
        public void DownloadDefaultCameraConfig()
        {
            
        }

        #region Debug stuff

        public string Debug_GetUserCamerasAsString()
        {
            List<Camera> cameras = GetUserCameras();

            List<string> stringlist = new List<string>();

            foreach (Camera camera in cameras)
            {
                stringlist.Add("Name: " + camera.Name);
                stringlist.Add("----------");
                stringlist.Add("URL: " + camera.URL);
                stringlist.Add("Owner: " + camera.Owner);
                stringlist.Add("Location: " + camera.Location);
                stringlist.Add("Camera type: " + camera.Type.ToString());
                stringlist.Add("");
            }

            string finalstring = "";

            int counter = 0;
            foreach (string line in stringlist)
            {
                if (counter == 0)
                    finalstring += line;
                else
                    finalstring += "\n" + line;

                counter++;
            }

            return finalstring;
        }

        public void Debug_CreateTestCameraConfig()
        {
            List<Camera> _list = new List<Camera>();

            for (int i = 0; i <= 10; i++)
            {
                Camera camera = new Camera()
                {
                    Name = "Camera #" + i,
                    URL = "testurl-" + i,
                    Owner = "Owner #" + i,
                    Location = "Location #" + i,
                    Type = CameraType.ImageCamera
                };

                _list.Add(camera);
            }

            using (StreamWriter file = File.CreateText(Utilities.GetPathForConfigurationFile("CameraConfig.json", Utilities.ConfigCategories.User)))
            {
                JsonSerializer serializer = new JsonSerializer() { Formatting = Formatting.Indented };
                serializer.Serialize(file, _list);
            }
        }

        #endregion
    }
}
