using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class AttackHelicopter : Helicopter, IPassengers, IMilitary
    {
        public AttackHelicopter(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, int maxLiftHeight, int countOfMembers, string number, string name, int tankVol, int maxSpeed, double cruisSpeed, int numOfPass, int maxAmmo, int currentAmmo)
            : base(brand, model, weight, releaseDate, lastMaintenance, maxLiftHeight, countOfMembers, number, name, tankVol, maxSpeed, cruisSpeed)
        {
            NumOfPassengers = numOfPass;
            MaxAmmo = maxAmmo;
            CurrentAmmo = currentAmmo;
        }

        public int NumOfPassengers { get; set; }
        public int MaxAmmo { get; set; }
        public int CurrentAmmo { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Number of passengers: {NumOfPassengers}");
            Console.WriteLine($"Max ammo: {MaxAmmo}");
            Console.WriteLine($"Cureent ammo: {CurrentAmmo}");
        }

        public void AcceptPassenger()
        {
            Console.WriteLine($"{Name} accept passengers");
        }

        public void DropOfPassenger()
        {
            Console.WriteLine($"{Name} drop off passengers");
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
