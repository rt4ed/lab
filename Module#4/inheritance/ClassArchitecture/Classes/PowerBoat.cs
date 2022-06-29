using ClassArchitecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Classes
{
    internal class PowerBoat : WaterTransport, INumerable, ILoadCapacity
    {
        public PowerBoat(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string name, int numOfPass, int boatDraft, int boardHeight, string number, int loadCapacity) 
            : base(brand, model, weight, releaseDate, lastMaintenance, name, numOfPass)
        {
            BoatDraft = boatDraft;
            BoardHeight = boardHeight;
            Number = number;
            LoadCapacity = loadCapacity;
        }
        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Boat draft: {BoatDraft}");
            Console.WriteLine($"Board height: {BoardHeight}");
            Console.WriteLine($"Number: {Number}");
            Console.WriteLine($"Load capacity: {LoadCapacity}");
        }
        public int BoatDraft { get; set; }
        public int BoardHeight { get; set; }
        public string Number { get; set; }
        public int LoadCapacity { get; set; }

    }
}
