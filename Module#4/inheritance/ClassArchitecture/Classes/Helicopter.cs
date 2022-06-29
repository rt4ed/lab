using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    abstract class Helicopter : AirTransport, IFuelTankVolume, ISpeedable, ICruisableSpeed
    {
        protected Helicopter(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, int maxLiftHeight, int countOfMembers, string number, string name, int tankVol,int maxSpeed, double cruisSpeed)
            : base(brand, model, weight, releaseDate, lastMaintenance, maxLiftHeight, countOfMembers, number, name)
        {
            TankVolume = tankVol;
            MaxSpeed = maxSpeed;
            CruisableSpeed = cruisSpeed;
        }

        public int TankVolume { get; set; }
        public int MaxSpeed { get; set; }
        public double CruisableSpeed { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Tank volume: {TankVolume}");
            Console.WriteLine($"Max speed: {MaxSpeed}");
            Console.WriteLine($"Cruisable speed: {CruisableSpeed}");
        }
    }
}
