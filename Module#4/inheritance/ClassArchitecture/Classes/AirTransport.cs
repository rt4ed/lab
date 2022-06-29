using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    abstract class AirTransport : Transport, ICrewMembers, IEnginable,  INumerable
    {
        
        protected AirTransport(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, int maxLiftHeight, int countOfMembers, string number, string name) : base(brand, model, weight, releaseDate, lastMaintenance, name)
        {
            MaxLiftHeight = maxLiftHeight;
            CountOfMembers = countOfMembers;
            Number = number;
        }

        public int MaxLiftHeight { get; set; }
        public int CountOfMembers { get; set; }
        public string Number { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Max lift height: {MaxLiftHeight}");
            Console.WriteLine($"Count of members: {CountOfMembers}");
            Console.WriteLine($"Number: {Number}");
        }
        public void RunDiagnostics()
        {
            Console.WriteLine($"{Name} run diagnistics");
        }

        public void StartEngine()
        {
            Console.WriteLine($"{Name} started engine");
        }

        public void StopEngine()
        {
            Console.WriteLine($"{Name} stopped engine");
        }

        public void Fly()
        {
            Console.WriteLine($"{Name} fly");
        }
        public void ToLand()
        {
            Console.WriteLine($"{Name} to land");
        }

        public void StartMove()
        {
            Console.WriteLine($"{Name} start move");
        }

        public void StopMove()
        {
            Console.WriteLine($"{Name} stop move");
        }
    }
}
