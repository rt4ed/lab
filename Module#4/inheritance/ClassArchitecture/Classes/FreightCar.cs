using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class FreightCar : GroundTransport, ILoadCapacity, IBootVolume, IFreightable
    {
        public FreightCar(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, string name, int bodyVolume, int loadCapacity, int volumeOfBoot, bool isSwitch, int engineVolume)
            : base(brand, model, weight, releaseDate, lastMaintenance, number, name)
        {
            BodyVolume = bodyVolume;
            LoadCapacity = loadCapacity;
            VolumeOfBoot = volumeOfBoot;
            IsSwitchable = isSwitch;
            EngineVolume = engineVolume;
        }

        public int BodyVolume { get; set; }
        public int LoadCapacity { get; set; }
        public int VolumeOfBoot { get; set; }
        public bool IsSwitchable { get; set; }
        public int EngineVolume { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Body volume: {BodyVolume}");
            Console.WriteLine($"Load capacity: {LoadCapacity}");
            Console.WriteLine($"Volume of boot: {VolumeOfBoot}");
            Console.WriteLine($"Is switchable: {IsSwitchable}");
            Console.WriteLine($"Engine volume: {EngineVolume}");
        }
        public void load()
        {
            Console.WriteLine($"{Name} load");
        }

        public void unload()
        {
            Console.WriteLine($"{Name} uload");
        }
    }
}
