using LibraryNetwork.Classes;
using LibraryNetwork.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryNetwork
{
    public class Book : ReadableObject
    {
        public Book(int id, string title, int pageCount, DateTime yearOfPublish, double price, string note, string authors,
            string cityOfPublish, string publisherName, int numberOfCopies, string isbn)
            : base(id, title, pageCount, yearOfPublish, price, note, cityOfPublish, publisherName, numberOfCopies)
        {
            Authors = authors;
            ISBN = isbn;
        }

        [Required(ErrorMessage = "Author not specified")]
        public string Authors { get; set; }

        public string ISBN { get; set; }
    }
}
