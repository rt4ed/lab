using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryNetwork.Interfaces
{
    public interface IGetData
    {
        public IDataManager<BaseStorageObject> GetDataManager(string path);
    }
}
