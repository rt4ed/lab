using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryNetwork
{
    public abstract class BaseStorageObject
    {
        protected BaseStorageObject(int id, string title, int pageCount, DateTime yearOfPublish, double price, string note)
        {
            Id = id;
            Title = title;
            PageCount = pageCount;
            YearOfPublish = yearOfPublish;
            Price = price;
            Note = note;
        }

        public int Id { get; private set; }

        [Required(ErrorMessage = "Name not specified")]
        public string Title { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of pages must be greater than or equal to 1")]
        public int PageCount { get; set; }

        [StringLength(500, ErrorMessage = "note exceeds 500 characters")]
        public string Note { get; set; }

        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Publication date cannot be earlier than 1900")]
        public virtual DateTime YearOfPublish { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "Cost not specified or less than 0")]
        public double Price { get; set; }
    }
}
