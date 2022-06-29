using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    abstract class GroundTransport : Transport, INumerable, IMovable
    {
        
        protected GroundTransport(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, string name) 
            : base(brand, model, weight, releaseDate, lastMaintenance, name)
        {
            Number = number;    
        }

        public string Number { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Number: {Number}");
        }
        public void StartMove()
        {
            Console.WriteLine($"{Name} started moving");
        }

        public void StopMove()
        {
            Console.WriteLine($"{Name} stopped moving");
        }
    }
}
