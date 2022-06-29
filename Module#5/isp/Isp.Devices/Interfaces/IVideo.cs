namespace Isp.Devices.Interfaces
{
    internal interface IVideo: IPhoto
    {
        public void StartVideoRecording();
        public void StopVideoRecording();
    }
}
