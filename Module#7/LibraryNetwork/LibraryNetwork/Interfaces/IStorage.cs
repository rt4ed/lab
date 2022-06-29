using System.Collections.Generic;

namespace LibraryNetwork.Interfaces
{
    public interface IStorage
    {
        void Add<T>(T obj) where T: BaseStorageObject;

        void Delete<T>(T obj) where T: BaseStorageObject;

        List<BaseStorageObject> GetStorageObjects<T>();
    }
}
