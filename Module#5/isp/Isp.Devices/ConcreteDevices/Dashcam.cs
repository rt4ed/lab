using System;
using System.IO;
using Isp.Devices.Interfaces;

namespace Isp.Devices.ConcreteDevices
{
    /// <summary>
    /// Видеорегистратор
    /// </summary>
    public class Dashcam : IVideo, IData
    {
        public void ExportData(Stream destination)
        {
            throw new NotImplementedException();
        }

        public void ImportData(Stream source)
        {
            throw new NotImplementedException();
        }

        public void StartVideoRecording()
        {
            throw new NotImplementedException();
        }

        public void StopVideoRecording()
        {
            throw new NotImplementedException();
        }

        public void TakePhoto()
        {
            throw new NotImplementedException();
        }
    }
}