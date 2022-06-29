using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class Airship : AirTransport, IPassengers, IMultiEngine, IFlightRange
    {
        public Airship(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, int maxLiftHeight, int countOfMembers, string number, string name, int shellVol, int shellLen, int numOfPass, int numOfEngine, int flRange)
            : base(brand, model, weight, releaseDate, lastMaintenance, maxLiftHeight, countOfMembers, number, name)
        {
            ShellVolume = shellVol;
            ShellLenght = shellLen;
            NumOfPassengers = numOfPass;
            NumberOfEngine = numOfEngine;
            FlightRange = flRange;
        }

        public int ShellVolume { get; set;  }
        public int ShellLenght { get; set; }
        public int NumOfPassengers { get; set; }
        public int NumberOfEngine { get; set; }
        public int FlightRange { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Shell volume: {ShellVolume}");
            Console.WriteLine($"Shell lenght: {ShellLenght}");
            Console.WriteLine($"Number of passengers: {NumOfPassengers}");
            Console.WriteLine($"Number of engine: {NumberOfEngine}");
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
