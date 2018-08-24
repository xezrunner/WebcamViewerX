using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebcamViewerX.Engine
{
    public enum CameraType
    {
        ImageCamera
    }

    public class Camera
    {
        public string Name;

        public string URL;

        public string Owner;

        public string Location;

        public CameraType Type;
    }
}
