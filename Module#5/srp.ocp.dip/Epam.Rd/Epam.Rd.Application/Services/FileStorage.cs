using Epam.Rd.Application.Interfaces;
using System;
using System.IO;
using System.Text.Json;

namespace Epam.Rd.Application.Services
{
	public class FileStorage: IStorage
	{
		public void Save<T>(T obj)
		{
			var path = Path.Combine("files", obj.GetType().FullName + Guid.NewGuid() + ".json");
			var jsonObj = JsonSerializer.Serialize(obj, new JsonSerializerOptions {WriteIndented = true});
			File.WriteAllText(path, jsonObj);
		}
	}
}