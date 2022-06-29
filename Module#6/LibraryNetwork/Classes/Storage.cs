using LibraryNetwork.Interfaces;
using System;

namespace LibraryNetwork
{
    public class Storage : IStorage
    {
        private BaseStorageObject[] StorageObjects = new BaseStorageObject[0];
        int i = 0;

        /// <summary>
        /// Добавление объекта в массив
        /// </summary>
        /// <param name="obj"></param>
        public void Add(BaseStorageObject obj)
        {
            Array.Resize(ref StorageObjects, i+1);
            StorageObjects[i] = obj;
            i++;
        }

        /// <summary>
        /// Удаление объекта из массива
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(BaseStorageObject obj)
        {
            BaseStorageObject[] newArr = new BaseStorageObject[StorageObjects.Length-1];

            int a = 0;
            for(int b = 0; b < StorageObjects.Length; b++) 
            { 
                if(!obj.Equals(StorageObjects[b]))
                {
                    newArr[a] = StorageObjects[b];
                    a++;
                }
            }

            StorageObjects = newArr;
            i--;
        }

        /// <summary>
        /// Показать все элементы массива
        /// </summary>
        public BaseStorageObject[] GetStorageObjects()
        {
            return StorageObjects;
        }



    }
}
