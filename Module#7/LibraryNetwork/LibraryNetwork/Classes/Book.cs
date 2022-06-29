using System;

namespace LibraryNetwork
{
    public class Book : BaseStorageObject
    {
        public Book(int id, string title, int pageCount, string authors, DateTime yearOfPublish, string cityOfPublish,
            string publisherName, string note, string isbn)
            : base(id, title, pageCount, yearOfPublish)
        {
            Authors = authors;
            CityOfPublish = cityOfPublish;
            PublisherName = publisherName;
            Note = note;
            ISBN = isbn;
        }

        public string Authors { get; set; }

        public string CityOfPublish { get; set; }

        public string PublisherName { get; set; }

        public string Note { get; set; }

        public string ISBN { get; set; }
    }
}
