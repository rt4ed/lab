using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class Tank : GroundTransport, ICrewMembers, IMovable, ISpeedable, IEnginable, IMilitary
    {
        public Tank(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, string name, int armor, int caliber, int barrel, int countOfMembers, int engineVolume,int maxSpeed, int maxAmmo, int currentAmmo)
            : base(brand, model, weight, releaseDate, lastMaintenance, number, name)
        {
            ArmorThickness = armor;
            GunCaliber = caliber;
            BarrelLength = barrel;
            CountOfMembers = countOfMembers;
            EngineVolume = engineVolume;
            MaxSpeed = maxSpeed;
            MaxAmmo = maxAmmo;
            CurrentAmmo = currentAmmo;
        }

        public int ArmorThickness { get; set; }
        public int GunCaliber { get; set; }
        public int BarrelLength { get; set; }
        public int CountOfMembers { get; set; }
        public int EngineVolume { get; set; }
        public int MaxSpeed { get; set; }
        public int MaxAmmo { get; set; }
        public int CurrentAmmo { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Armor thickness: {ArmorThickness}");
            Console.WriteLine($"Gun caliber: {GunCaliber}");
            Console.WriteLine($"Barrel length: {BarrelLength}");
            Console.WriteLine($"Count of members: {CountOfMembers}");
            Console.WriteLine($"Engine volume: {EngineVolume}");
            Console.WriteLine($"Max speed: {MaxSpeed}");
            Console.WriteLine($"Max ammo: {MaxAmmo}");
            Console.WriteLine($"Current ammo: {CurrentAmmo}");
        }
        public void GetAmmo()
        {
            Console.WriteLine($"{Name} get ammo");
        }

        public void OpenFire()
        {
            Console.WriteLine($"{Name} open fire");
            CurrentAmmo--;
        }

        public void RunDiagnostics()
        {
            Console.WriteLine($"{Name} run diagnostics");
        }

        public void StartEngine()
        {
            Console.WriteLine($"{Name} start engine");
        }

        public void StartOffensive()
        {
            Console.WriteLine($"{Name} start offensive");
        }

        public void StartRetreat()
        {
            Console.WriteLine($"{Name} start retreat");
        }

        public void StopEngine()
        {
            Console.WriteLine($"{Name} stop engine");
        }

        public void StopOffensive()
        {
            Console.WriteLine($"{Name} stop offensive");
        }

        public void StopRetreat()
        {
            Console.WriteLine($"{Name} stop retreat");
        }
    }
}
