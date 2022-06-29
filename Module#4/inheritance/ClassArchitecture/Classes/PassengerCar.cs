using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class PassengerCar : Car, IPassengers, IBootVolume
    {
        public PassengerCar(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, int tankVolume, string category, string bodyType, int numOfPass, int volumeOfBoot, bool isSwithable, int engineVolume, string name)
            : base(brand, model, weight, releaseDate, lastMaintenance, number, tankVolume, category, name)
        {
            BodyType = bodyType;
            NumOfPassengers = numOfPass;
            VolumeOfBoot = volumeOfBoot;
            EngineVolume = engineVolume;
            IsSwitchable = isSwithable;
        }

        
        public string BodyType { get; set; }
        public int NumOfPassengers { get; set; }
        public int VolumeOfBoot { get; set; }
        public bool IsSwitchable { get; set; }
        public int EngineVolume { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Body type: {BodyType}");
            Console.WriteLine($"Number of passengers: {NumOfPassengers}");
            Console.WriteLine($"Volime of boot: {VolumeOfBoot}");
            Console.WriteLine($"Have switch: {IsSwitchable}");
            Console.WriteLine($"Engine volume: {EngineVolume}");
        }
        public void AcceptPassenger()
        {
            Console.WriteLine($"{Name} accept passenger");
        }

        public void DropOfPassenger()
        {
            Console.WriteLine($"{Name} car drop off passenger");
        }

        
        
    }
    
}
