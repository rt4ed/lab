using LibraryNetwork.Interfaces;
using System.Collections.Generic;

namespace LibraryNetwork
{
    public class Storage : IStorage
    {
        private List<BaseStorageObject> storageList = new List<BaseStorageObject>();

        /// <summary>
        /// Добавление объекта в массив
        /// </summary>
        /// <param name="obj"></param>
        public void Add<T>(T obj) where T: BaseStorageObject
        {
            storageList.Add(obj);
        }

        /// <summary>
        /// Удаление объекта из массива
        /// </summary>
        /// <param name="obj"></param>
        public void Delete<T>(T obj) where T: BaseStorageObject
        {
            storageList.Remove(obj);
        }

        /// <summary>
        /// Показать все элементы массива
        /// </summary>
        public List<BaseStorageObject> GetStorageObjects<T>()
        {
            return storageList;
        }
    }
}
