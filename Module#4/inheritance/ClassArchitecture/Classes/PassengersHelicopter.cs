using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class PassengersHelicopter : Helicopter, IPassengers, IFlightRange
    {
        public PassengersHelicopter(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, int maxLiftHeight, int countOfMembers, string number, string name, int tankVol, int maxSpeed, double cruisSpeed, int numOfPass, int flRange)
            : base(brand, model, weight, releaseDate, lastMaintenance, maxLiftHeight, countOfMembers, number, name, tankVol, maxSpeed, cruisSpeed)
        {
            NumOfPassengers = numOfPass;
            FlightRange = flRange;
        }

        public int NumOfPassengers { get; set; }
        public int FlightRange { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Number of passengers: {NumOfPassengers}");
            Console.WriteLine($"Flight range: {FlightRange}");
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
