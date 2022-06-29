using System;
using System.IO;
using Isp.Devices.Interfaces;

namespace Isp.Devices.ConcreteDevices
{
	/// <summary>
	/// Смартфон
	/// </summary>
	public class SmartPhone : IApplication, IVideo, IData, IMessage
	{
		public void RunApplication(string application)
		{
			Console.WriteLine($"Run application {application}.");
		}

		public void TakePhoto()
		{
			Console.WriteLine("Take photo.");
		}

		public void StartVideoRecording()
		{
			Console.WriteLine("Start recording video.");
		}

		public void StopVideoRecording()
		{
			Console.WriteLine("Stop recording video.");
		}

		public void Call(string number)
		{
			Console.WriteLine($"Calling {number}...");
		}

		public void SendMessage(string number, string message)
		{
			Console.WriteLine($"Send message '{message}' to {number}");
		}

		public void ExportData(Stream destination)
		{
			Console.WriteLine("Export some data");
		}

		public void ImportData(Stream source)
		{
			Console.WriteLine("Import some data");
		}
	}
}