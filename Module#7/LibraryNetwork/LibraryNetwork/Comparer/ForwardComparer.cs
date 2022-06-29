using System.Collections.Generic;

namespace LibraryNetwork.Classes
{
    internal class ForwardComparer<T> : IComparer<T>
        where T : BaseStorageObject
    {
        public int Compare(T item1, T item2)
        {
            if(item1?.YearOfPublish > item2?.YearOfPublish)
                return 1;
            if (item1?.YearOfPublish < item2?.YearOfPublish)
                return -1;
            return 0;
        }
    }
}
