using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    abstract class PublicTransport : GroundTransport, IPassengers, ICategoryable, ISwitchable, IPublic, IEnginable
    {
        protected PublicTransport(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, string name, int numOfPass, string category, bool isSwitch, int engineVolume, bool conductor, int routeNum)
            : base(brand, model, weight, releaseDate, lastMaintenance, number, name)
        {
            NumOfPassengers = numOfPass;
            Category = category;
            IsSwitchable = isSwitch;
            EngineVolume = engineVolume;
            Conductor = conductor;
            RouteNumber = routeNum;
        }

        public int NumOfPassengers { get; set; }
        public string Category { get; set; }
        public bool IsSwitchable { get; set; }
        public int EngineVolume { get; set; }
        public bool Conductor { get; set; }
        public int RouteNumber { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Num of passengers: {NumOfPassengers}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Is switchable: {IsSwitchable}");
            Console.WriteLine($"Engine volume: {EngineVolume}");
            Console.WriteLine($"Conductor: {Conductor}");
            Console.WriteLine($"Route number: {RouteNumber}");
        }
        public void AcceptPassenger()
        {
            Console.WriteLine($"{Name} accept passengers");
        }

        public void DropOfPassenger()
        {
            Console.WriteLine($"{Name} drop off passengers");
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
