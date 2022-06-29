using System;

namespace LibraryNetwork
{
    public abstract class BaseStorageObject : IComparable
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

        public int CompareTo(object? o)
        {
            if (o is BaseStorageObject obj) return YearOfPublish.Year.CompareTo(obj.YearOfPublish.Year);
            else throw new ArgumentException("Некорректное значение параметра");
        }
    }
}
