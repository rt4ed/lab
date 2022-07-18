using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryNetwork.Classes
{
    public class ReadableObject : BaseStorageObject
    {
        protected ReadableObject(int id, string title, int pageCount, DateTime yearOfPublish, double price, string note,
             string cityOfPublish, string publisherName, int numberOfCopies)
            : base(id, title, pageCount, yearOfPublish, price, note)
        {
            CityOfPublish = cityOfPublish;
            PublisherName = publisherName;
            NumberOfCopies = numberOfCopies;
        }

        [Required(ErrorMessage = "City of publish not specified")]
        public string CityOfPublish { get; set; }

        [Required(ErrorMessage = "Publisher name not specified")]
        public string PublisherName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The number of copies cannot be less than 0")]
        public int NumberOfCopies { get; set; }
    }
}
