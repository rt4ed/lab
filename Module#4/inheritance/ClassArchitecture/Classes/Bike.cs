using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class Bike: GroundTransport
    {
        public Bike(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, string name, bool frontBrake, bool backBrake, bool frontDerailleur, bool backDerailleur, bool frame)
            : base(brand, model, weight, releaseDate, lastMaintenance, number, name)
        {
            FrontBrake = frontBrake;
            BackBrake = backBrake;
            FrontDerailleur = frontDerailleur;
            BackDerailleur = backDerailleur;
            Frame = frame;
        }

        public bool FrontBrake { get; set; }
        public bool BackBrake { get; set; }
        public bool FrontDerailleur { get; set; }
        public bool BackDerailleur { get; set; }
        public bool Frame { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Front brake: {FrontBrake}");
            Console.WriteLine($"Back breke: {BackBrake}");
            Console.WriteLine($"Front derailleur: {FrontDerailleur}");
            Console.WriteLine($"Back derailleur: {BackDerailleur}");
            Console.WriteLine($"Frame: {Frame}");
        }
    }
}
