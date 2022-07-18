using LibraryNetwork.Interfaces;
using LibraryNetwork.Serializer;
using LibraryNetwork.Serializers;

namespace LibraryNetwork.Entities
{
    public class DataFactory : IGetData
    {
        public IDataManager<BaseStorageObject> GetDataManager(string path)
        {
            if(path.Contains(".xml"))
            {
                return new XMLSerializer();
            }
            if (path.Contains(".json"))
            {
                return new JSONSerializer();
            }
            return null;
        }
    }
}
