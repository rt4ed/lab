using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class FireHelicopter : Helicopter, IFlightRange, IFireFighting
    {
        public FireHelicopter(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, int maxLiftHeight, int countOfMembers, string number, string name, int tankVol, int maxSpeed, double cruisSpeed, int flRange, int volOfWater, int firefightMem)
            : base(brand, model, weight, releaseDate, lastMaintenance, maxLiftHeight, countOfMembers, number, name, tankVol, maxSpeed, cruisSpeed)
        {
            FlightRange = flRange;
            VolumeOfWater = volOfWater;
            FireFigtingsMembers = firefightMem;
        }

        public int FlightRange { get; set; }
        public int VolumeOfWater { get; set; }
        public int FireFigtingsMembers { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Flight range: {FlightRange}");
            Console.WriteLine($"Volume of water: {VolumeOfWater}");
            Console.WriteLine($"Firefightings members: {FireFigtingsMembers}");
        }
        public void ExtinguishAFire()
        {
            Console.WriteLine($"{Name} extinguish a fire");
        }
    }
}
