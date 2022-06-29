using System;

namespace LibraryNetwork
{
    public class Patent : BaseStorageObject
    {
        public Patent(int id, string name, int pageCount,DateTime yearOfPublish, string creator, string country, int regNum,
            DateTime appDate, string note)
            : base(id, name, pageCount, yearOfPublish)
        {
            Creator = creator;
            Country = country;
            RegistrationNumber = regNum;
            ApplicationDate = appDate;
            Note = note;
        }

        public string Creator { get; set; }

        public string Country { get; set; }

        public int RegistrationNumber { get; set; }

        public DateTime ApplicationDate { get; set; }

        public string Note { get; set; }
    }
}
