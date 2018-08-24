using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebcamViewerX.Engine
{
    public class ImageCameraSaveUtils
    {
        public ImageCameraSaveUtils()
        {
            Client.DownloadProgressChanged += Client_DownloadProgressChanged;
            Client.DownloadFileCompleted += Client_DownloadFileCompleted;
        }

        WebClient Client = new WebClient();

        string GetDummy()
        {
            return "?dummy=" + DateTime.Now.Ticks;
        }

        // TODO: custom file browser?
        public SaveFileDialog dialog = new SaveFileDialog()
        {
            Title = "Save image",
            Filter = "JPG image (*.jpg)|*.jpg|All files (*.*)|*.*",
            DefaultExt = "jpg",
        };

        public async Task SaveImageFile(Camera camera)
        {
            bool? result = dialog.ShowDialog();

            if (!result.HasValue || !result.Value)
                return;

            string url = camera.URL + GetDummy();
            string localPath = dialog.FileName;

            await Client.DownloadFileTaskAsync(url, localPath);
        }

        public event EventHandler ProgressChanged;

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, null);
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw new Exception("Couldn't download image: " + e.Error.Message);
            }
        }
    }
}
