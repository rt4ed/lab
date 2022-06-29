using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class Fighter : Airplane, IMilitary
    {
        public Fighter(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, int maxLiftHeight, int countOfMembers, string number, string name, int len, int wingspan, int chassisDist, int numOfEngine, int tankVol, int maxAmmo, int currentAmmo)
            : base(brand, model, weight, releaseDate, lastMaintenance, maxLiftHeight, countOfMembers, number, name, len, wingspan, chassisDist, numOfEngine, tankVol)
        {
            MaxAmmo = maxAmmo;
            CurrentAmmo = currentAmmo;
        }

        public int MaxAmmo { get; set; }
        public int CurrentAmmo { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
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

        public void StartOffensive()
        {
            Console.WriteLine($"{Name} start offensive");
        }

        public void StartRetreat()
        {
            Console.WriteLine($"{Name} start retreat");
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
