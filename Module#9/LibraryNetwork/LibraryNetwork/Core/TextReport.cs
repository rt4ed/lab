using LibraryNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryNetwork.Core
{
    public class TextReport : IReport
    {
        public void Create(string path, IEnumerable<BaseStorageObject> objectsCollection)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Dictionary<Type, ICollection<BaseStorageObject>> dictionary = new Dictionary<Type, ICollection<BaseStorageObject>>();

            foreach (var baseObj in objectsCollection)
            {
                if (!dictionary.ContainsKey(baseObj.GetType()))
                    dictionary.Add(baseObj.GetType(), new List<BaseStorageObject>());

                dictionary[baseObj.GetType()].Add(baseObj);
            }

            foreach (var key in dictionary)
            {
                stringBuilder.AppendLine(key.Key.Name);
                foreach (var value in key.Value)
                {
                    stringBuilder.AppendLine($"{value.Title,10}\t\t\t{value.YearOfPublish}");
                }
                stringBuilder.AppendLine($"{key.Value.Count}");
                stringBuilder.AppendLine("");
            }

            try
            {
                using (var sw = new StreamWriter(path, false))
                {
                    sw.Write(stringBuilder);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException("Wrong path", ex);
            }
        }
    }
}
