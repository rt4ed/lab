using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class Motorcycle : GroundTransport, IMovable, ISpeedable, IFuelTankVolume, IEnginable
    {
        public Motorcycle(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, string name, int tankVolume, int engineVolume, int maxSpeed)
            : base(brand, model, weight, releaseDate, lastMaintenance, number, name)
        {
            TankVolume = tankVolume;
            EngineVolume = engineVolume;   
            MaxSpeed = maxSpeed;
        }

        public int TankVolume { get; set; }
        public int EngineVolume { get; set; }
        public int MaxSpeed { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Tank volume: {TankVolume}");
            Console.WriteLine($"Engine volume: {EngineVolume}");
            Console.WriteLine($"Max speed: {MaxSpeed}");
        }
        public void RunDiagnostics()
        {
            Console.WriteLine($"{Name} run diagnostics");
        }

        public void StartEngine()
        {
            Console.WriteLine($"{Name} start engine");
        }

        public void StopEngine()
        {
            Console.WriteLine($"{Name} stop engine");
        }
    }
}
