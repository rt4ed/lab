using System.Collections.Generic;

namespace LibraryNetwork.Interfaces
{
    public interface IReport
    {
        void Create(string path, IEnumerable<BaseStorageObject> objectsCollection);
    }
}
