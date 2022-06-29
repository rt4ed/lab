using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    abstract class WaterTransport: Transport, IMovable, IPassengers
    {
        protected WaterTransport(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string name, int numOfPass) : base(brand, model, weight, releaseDate, lastMaintenance, name)
        {
            NumOfPassengers = numOfPass;
        }

        public int NumOfPassengers { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Num of passengers: {NumOfPassengers}");
        }
        public void SetSailShore()
        {
            Console.WriteLine($"{Name} set sail shore");
        }

        public void MoorShore()
        {
            Console.WriteLine($"{Name} moor shore");
        }

        public void StartMove()
        {
            Console.WriteLine($"{Name} started move");
        }

        public void StopMove()
        {
            Console.WriteLine($"{Name} stopped move");
        }

        public void AcceptPassenger()
        {
            Console.WriteLine($"{Name} accept passsengers");
        }

        public void DropOfPassenger()
        {
            Console.WriteLine($"{Name} drop of passengers");
        }
    }
}
