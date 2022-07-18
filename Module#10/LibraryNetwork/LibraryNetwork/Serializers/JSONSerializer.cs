using LibraryNetwork.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace LibraryNetwork.Serializers
{
    public class JSONSerializer : IDataManager<BaseStorageObject>
    {
        public IEnumerable<BaseStorageObject> GetData(string path)
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(IEnumerable<BaseStorageObject>));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                var baseStorageObjects = jsonFormatter.ReadObject(fs) as IEnumerable<BaseStorageObject>;
                return baseStorageObjects;
            }

        }

        public void SaveData(IEnumerable<BaseStorageObject> data, string path)
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(IEnumerable<BaseStorageObject>));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, data);
            }
        }
    }
}
