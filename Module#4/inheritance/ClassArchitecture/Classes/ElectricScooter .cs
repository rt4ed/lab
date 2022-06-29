using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class ElectricScooter: GroundTransport
    {
        

        public ElectricScooter(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string number, string name, int batteryCapacity)
            : base(brand, model, weight, releaseDate, lastMaintenance, number, name)
        {
            BatteryCapacity = batteryCapacity;
        }

        public int BatteryCapacity { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Battery capacity: {BatteryCapacity}");
        }
        public void BatteryCharge()
        {
            Console.WriteLine($"{Name} start battery charge");
        }
    }
}
