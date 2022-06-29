using System;
using System.IO;
using Isp.Devices.Interfaces;

namespace Isp.Devices.ConcreteDevices
{
    public class SmartSpeaker : IMessage, IData
    {
        public void Call(string number)
        {
            throw new NotImplementedException();
        }

        public void ExportData(Stream destination)
        {
            throw new NotImplementedException();
        }

        public void ImportData(Stream source)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string number, string message)
        {
            throw new NotImplementedException();
        }
    }
}