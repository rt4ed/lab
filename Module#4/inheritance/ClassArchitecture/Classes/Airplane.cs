using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    abstract class Airplane: AirTransport, IMultiEngine, IMovable, IFuelTankVolume
    {
        protected Airplane(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, int maxLiftHeight, int countOfMembers, string number, string name, int len, int wingspan, int chassisDist, int numOfEngine, int tankVol)
            : base(brand, model, weight, releaseDate, lastMaintenance, maxLiftHeight, countOfMembers, number, name)
        {
            TakeoffLength = len;
            Wingspan = wingspan;
            ChassisDistance = chassisDist;
            NumberOfEngine = numOfEngine;
            TankVolume = tankVol;
        }

        public int TakeoffLength { get; set; }
        public int Wingspan { get; set; }
        public int ChassisDistance { get; set; }

        public int NumberOfEngine { get; set; }
        public int TankVolume { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Take off lenght: {TakeoffLength}");
            Console.WriteLine($"Wingspan: {Wingspan}");
            Console.WriteLine($"Chassis distance: {ChassisDistance}");
            Console.WriteLine($"Number of engine: {NumberOfEngine}");
            Console.WriteLine($"Tank volume: {TankVolume}");
        }
        public void GetClassOfAirfield()
        {
            Console.WriteLine($"{Name} has aerodrome class");
        }

        public new void StartMove()
        {
            Console.WriteLine($"{Name} started move");
        }

        public new void StopMove()
        {
            Console.WriteLine($"{Name} stopped move"); 
        }
    }
}
