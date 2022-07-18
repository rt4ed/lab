using LibraryNetwork.EventHandler;
using System;
using System.Collections.Generic;

namespace LibraryNetwork.Interfaces
{
    public interface IStorage
    {
        event EventHandler<BaseStorageObjectArgs> FundAdded;
        event EventHandler<MyCancelEventArgs> FundDeleting;

        void Add<T>(T obj) where T: BaseStorageObject;

        void Delete<T>(T obj) where T: BaseStorageObject;

        IEnumerable<BaseStorageObject> GetStorageObjects<T>();
    }
}
