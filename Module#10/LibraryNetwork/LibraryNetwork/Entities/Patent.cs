using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryNetwork
{
    [Serializable]
    public class Patent : BaseStorageObject
    {
        public Patent(int id, string name, int pageCount,DateTime yearOfPublish, double price, string creator, string country, int regNum,
            DateTime appDate, string note)
            : base(id, name, pageCount, yearOfPublish, price, note)
        {
            Creator = creator;
            Country = country;
            RegistrationNumber = regNum;
            ApplicationDate = appDate;
        }

        [Required(ErrorMessage = "Creator not specified")]
        public string Creator { get; set; }

        [Required(ErrorMessage = "Country not specified")]
        public string Country { get; set; }

        public int RegistrationNumber { get; set; }

        [Required, Range(typeof(DateTime), "1/1/1950", "1/1/2100", ErrorMessage = "Publication date cannot be earlier than 1950")]
        public DateTime ApplicationDate { get; set; }

        [Required, Range(typeof(DateTime), "1/1/1950", "1/1/2100", ErrorMessage = "Publication date cannot be earlier than 1950")]
        public override DateTime YearOfPublish { get; set; }
    }
}
