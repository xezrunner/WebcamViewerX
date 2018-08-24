using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WebcamViewerX.Engine
{
    public class CameraBitmapImageGatherer
    {
        string GetDummy()
        {
            return "?dummy=" + DateTime.Now.Ticks;
        }

        public async Task<BitmapImage> GetCameraImage(Camera camera)
        {
            BitmapImage image = null;

            byte[] bytes = null;

            try
            {
                using (WebClient client = new WebClient())
                {
                    bytes = await client.DownloadDataTaskAsync(camera.URL + GetDummy());
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;
                }

                image = new BitmapImage();

                if (bytes.Length != 0)
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.None;
                    image.StreamSource = new MemoryStream(bytes);
                    image.EndInit();
                }
                else
                    throw new Exception("The camera image is null.");

                return image;
            }
            catch (WebException ex)
            {
                throw new Exception("Couldn't load image: " + ex.Message);
            }
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
                throw new Exception("Download failed: " + e.Error.Message);
        }
    }
}
