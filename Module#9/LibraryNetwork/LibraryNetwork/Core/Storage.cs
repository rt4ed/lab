using LibraryNetwork.EventHandler;
using LibraryNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LibraryNetwork
{
    public class Storage : IStorage
    {
        private List<BaseStorageObject> _storageList =  new List<BaseStorageObject>();
        public event EventHandler<BaseStorageObjectArgs> FundAdded;
        public event EventHandler<MyCancelEventArgs> FundDeleting;

        /// <summary>
        /// Добавление объекта в массив
        /// </summary>
        /// <param name="obj"></param>
        public void Add<T>(T obj) where T: BaseStorageObject
        {
            _storageList.Add(obj);
            FundAdded(this, new BaseStorageObjectArgs(obj.GetType()));
        }

        /// <summary>
        /// Удаление объекта из массива
        /// </summary>
        /// <param name="obj"></param>
        public void Delete<T>(T obj) where T: BaseStorageObject
        {
            FundDeleting(this, new MyCancelEventArgs(obj.GetType(), obj.Id, obj.Title));
            _storageList.Remove(obj);
        }

        /// <summary>
        /// Показать все элементы массива
        /// </summary>
        public IEnumerable<BaseStorageObject> GetStorageObjects<T>()
        {
            return _storageList;
        }
    }
}
