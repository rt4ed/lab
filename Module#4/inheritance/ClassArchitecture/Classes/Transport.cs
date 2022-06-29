using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    abstract class Transport
    {
        public Transport(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string name)
        {
            Brand = brand;
            Model = model;
            Weight = weight;
            ReleaseDate = releaseDate;
            LastMaintenance = lastMaintenance;
            Name = name;
        }


        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Weight { get; set; }
        public DateTime ReleaseDate { get; set; }   
        public DateTime LastMaintenance { get; set; }   

        public virtual void GetDiscription()
        {
            Console.WriteLine($"{Name} discription :");
            Console.WriteLine($"Brand: {Brand}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"Weight: {Weight}");
            Console.WriteLine($"Release date:{ReleaseDate}");
            Console.WriteLine($"Last maintenance: {LastMaintenance}");
        }

        public virtual void GetTechInspection()
        {
            Console.WriteLine($"{Name} passed technical inspection");
        }
    }
}
