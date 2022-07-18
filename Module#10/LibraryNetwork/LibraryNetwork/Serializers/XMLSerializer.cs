using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LibraryNetwork.Interfaces;

namespace LibraryNetwork.Serializer
{
    public class XMLSerializer: IDataManager<BaseStorageObject>
    {
        public IEnumerable<BaseStorageObject> GetData(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(IEnumerable<BaseStorageObject>));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                IEnumerable<BaseStorageObject> baseStorageObjects = xmlSerializer.Deserialize(fs) as IEnumerable<BaseStorageObject>;
                    return baseStorageObjects;
            }
        }

        public void SaveData(IEnumerable<BaseStorageObject> data, string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(IEnumerable<BaseStorageObject>));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, data);
            }
        }
    }
}
