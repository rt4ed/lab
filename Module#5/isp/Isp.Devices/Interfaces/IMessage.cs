namespace Isp.Devices.Interfaces
{
    internal interface IMessage: ICall
    {
        public void SendMessage(string number, string message);
    }
}
