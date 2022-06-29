using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class TransportAirplane : Airplane, ILoadCapacity, IFreightable
    {
        public TransportAirplane(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, int maxLiftHeight, int countOfMembers, string number, string name, int len, int wingspan, int chassisDist, int numOfEngine, int tankVol, int compartment, int loadCap)
            : base(brand, model, weight, releaseDate, lastMaintenance, maxLiftHeight, countOfMembers, number, name, len, wingspan, chassisDist, numOfEngine, tankVol)
        {
            CompartmentVolume = compartment;
            LoadCapacity = loadCap;
        }

        public int CompartmentVolume { get; set; }
        public int LoadCapacity { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Compartment volume: {CompartmentVolume}");
            Console.WriteLine($"Load capacity: {LoadCapacity}");
        }
        public void load()
        {
            Console.WriteLine($"{Name} load");
        }

        public void unload()
        {
            Console.WriteLine($"{Name} unload");
        }
    }
}
