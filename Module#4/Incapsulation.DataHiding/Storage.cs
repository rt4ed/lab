using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Incapsulation.DataHiding
{
	public class Storage<T> where T : class
	{
		private const int DataLength = 10000;
		private string FilePath;

		public Storage()
		{
			FilePath = Path.GetTempFileName();
		}

		public int Add(T data)
		{
			using var stream = OpenFile();

			BinaryFormatter bf = new BinaryFormatter();
			using MemoryStream ms = new MemoryStream();

			bf.Serialize(ms, data);
			stream.Seek(0, SeekOrigin.End);
			stream.Write(ms.ToArray());
			stream.Write(new byte[DataLength - ms.Length]);
			var key = stream.Position;

			stream.Flush();
			stream.Close();

			return (int)key;
		}

		private Stream OpenFile()
		{
			return File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
		}
	}
}