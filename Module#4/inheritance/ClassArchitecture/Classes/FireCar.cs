using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class FireCar : Car, IFireFighting
    {
        public FireCar(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, int tankVolume, string category, string name, int volumeOfWater, int fireFightingMembers)
            : base(brand, model, weight, releaseDate, lastMaintenance, number, tankVolume, category, name)
        {
            VolumeOfWater = volumeOfWater;
            FireFigtingsMembers = fireFightingMembers;
        }

        public int VolumeOfWater { get; set; }
        public int FireFigtingsMembers { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Volume of water: {VolumeOfWater}");
            Console.WriteLine($"Firefightings members: {FireFigtingsMembers}");
        }
        public void ExtinguishAFire()
        {
            Console.WriteLine($"{Name} extigushing a fire");
        }
    }
}
