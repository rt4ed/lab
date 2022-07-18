using System.Collections.Generic;

namespace LibraryNetwork.Interfaces
{
    public interface IDataManager<T>
    {
        IEnumerable<T> GetData(string path); // помните про yield return

        void SaveData(IEnumerable<T> data, string path);
    }

}
