using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class Bus : PublicTransport, IFuelTankVolume
    {
        public Bus(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, string name, int numOfPass, string category, bool isSwitch, int engineVolume, bool conductor, int routeNum, int tankVolume)
            : base(brand, model, weight, releaseDate, lastMaintenance, number, name, numOfPass, category, isSwitch, engineVolume, conductor, routeNum)
        {
            TankVolume = tankVolume;
        }

        public int TankVolume { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Tank volume: {TankVolume}");
        }
    }
}
