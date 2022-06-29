using System;

namespace LibraryNetwork
{
    public class Newspaper : BaseStorageObject
    {
        public Newspaper(int id, string title, int pageCount, DateTime yearOfPublish, string placeOfPublication, string titleOfPublisher,
            string note, int number, DateTime date, string issn)
            : base(id, title, pageCount, yearOfPublish)
        {
            PlaceOfPublication = placeOfPublication;
            TitleOfPublisher = titleOfPublisher;
            Note = note;
            Number = number;
            Date = date;
            Issn = issn;
        }

        public string PlaceOfPublication { get; set; }

        public string TitleOfPublisher { get; set; }
        
        public string Note { get; set; }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        public string Issn { get; set; }
    }
}
