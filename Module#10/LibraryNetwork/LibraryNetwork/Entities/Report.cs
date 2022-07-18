using LibraryNetwork.Interfaces;
using System.Collections.Generic;

namespace LibraryNetwork.Classes
{
    internal class Report 
    {
        public Report(string path, IReport reportStrategy)
        {
            Path = path;
            ReportStartegy = reportStrategy;
        }

        public IReport ReportStartegy { private get; set; }

        public string Path { get; set; }

        public void Create(IEnumerable<BaseStorageObject> objectsCollection)
        {
            ReportStartegy.Create(Path, objectsCollection);
        }
    }
}
