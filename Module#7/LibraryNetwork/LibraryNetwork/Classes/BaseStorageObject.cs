using System;

namespace LibraryNetwork
{
    public abstract class BaseStorageObject
    {
        protected BaseStorageObject(int id, string title, int pageCount, DateTime yearOfPublish)
        {
            Id = id;
            Title = title;
            PageCount = pageCount;
            YearOfPublish = yearOfPublish;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int PageCount { get; set; }

        public DateTime YearOfPublish { get; set; }
    }
}
