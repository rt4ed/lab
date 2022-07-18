using LibraryNetwork.Classes;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryNetwork
{
    [Serializable]
    public class Newspaper : ReadableObject
    {
        public Newspaper(int id, string title, int pageCount, DateTime yearOfPublish, double price, string note,
            string cityOfPublish, string publisherName, int numberOfCopies, int number, DateTime date, string issn)
            : base(id, title, pageCount, yearOfPublish, price, note, cityOfPublish, publisherName, numberOfCopies)
        {
            Number = number;
            Date = date;
            ISSN = issn;
        }

        [Range(1, int.MaxValue, ErrorMessage = "Number is not positive")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Date not specified")]
        public DateTime Date { get; set; }

        public string ISSN { get; set; }
    }
}
