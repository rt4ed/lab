using System.IO;

namespace Isp.Devices.Interfaces
{
    internal interface IData
    {
        public void ExportData(Stream destination);
        public void ImportData(Stream source);
    }
}
