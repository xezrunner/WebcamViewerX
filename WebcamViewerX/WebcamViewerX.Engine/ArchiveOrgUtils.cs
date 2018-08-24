using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebcamViewerX.Engine
{
    public class ArchiveOrgUtils
    {
        public async Task SaveOnArchiveOrg(Camera camera)
        {
            string url = "http://web.archive.org/save/" + camera.URL;

            try
            {
                WebRequest request = WebRequest.Create(url);

                using (WebResponse _response = await request.GetResponseAsync())
                {
                    HttpWebResponse response = (HttpWebResponse)_response;

                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception("Couldn't save to archive.org: " + (int)response.StatusCode + " " + response.StatusCode.ToString());
                }
            }
            catch (WebException ex)
            {
                throw new Exception("Couldn't save to archive.org: " + ex.Message);
            }
        }
    }
}
