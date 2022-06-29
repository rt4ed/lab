using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class ElectricLocomotive: GroundTransport
    {
        
        public ElectricLocomotive(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, string name, int force, int minRadius)
            : base(brand, model, weight, releaseDate, lastMaintenance, number, name)
        {
            TracrionForce = force;
            MinRadius = minRadius;
        }

        public int TracrionForce { get; set; }
        public int MinRadius { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Tracrion force: {TracrionForce}");
            Console.WriteLine($"Min radius: {MinRadius}");
        }
    }
}
