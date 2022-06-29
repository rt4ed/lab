using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class PassengersAirplane : Airplane, IPassengers
    {
        public PassengersAirplane(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, int maxLiftHeight, int countOfMembers, string number, string name, int len, int wingspan, int chassisDist, int numOfEngine, int tankVol, int numOfPass)
            : base(brand, model, weight, releaseDate, lastMaintenance, maxLiftHeight, countOfMembers, number, name, len, wingspan, chassisDist, numOfEngine, tankVol)
        {
            NumOfPassengers = numOfPass;
        }

        public int NumOfPassengers { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Number of passengers: {NumOfPassengers}");
        }
        public void AcceptPassenger()
        {
            Console.WriteLine($"{Name} accept passengers");
        }

        public void DropOfPassenger()
        {
            Console.WriteLine($"{Name} drop off passengers");
        }
    }
}
