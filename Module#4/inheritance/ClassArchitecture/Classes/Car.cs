using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    abstract class Car : GroundTransport, IFuelTankVolume, ICategoryable, IEnginable
    {
        
        protected Car(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, int tankVolume, string category, string name) : base(brand, model, weight, releaseDate, lastMaintenance, number, name)
        {
            TankVolume = tankVolume;
            Category = category;
        }

        public int TankVolume { get; set; }
        public string Category { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"TankVolume: {TankVolume}");
            Console.WriteLine($"Category: {Category}");
        }
        public void RunDiagnostics()
        {
            Console.WriteLine($"{Name}  passed diagnostics");
        }

        public void StartEngine()
        {
            Console.WriteLine($"{Name}  started the engine");
        }

        public void StopEngine()
        {
            Console.WriteLine($"{Name}  stopped the engine");
        }
    }
}
