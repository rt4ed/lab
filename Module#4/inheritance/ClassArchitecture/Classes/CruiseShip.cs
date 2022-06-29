using ClassArchitecture.Interfaces;
using System;

namespace ClassArchitecture.Classes
{
    internal class CruiseShip : WaterTransport, ICrewMembers
    {
        public CruiseShip(string brand, string model, int weight, DateTime releaseDate, DateTime lastMaintenance, string name, int numOfPass,
            string nameOfShip, int numOfCabins, int countOfMembers)
            : base(brand, model, weight, releaseDate, lastMaintenance, name, numOfPass)
        {
            NameOfShip = nameOfShip;
            NumberOfCabins = numOfCabins;
            CountOfMembers = countOfMembers;
        }

        public string NameOfShip { get; set; }

        public int NumberOfCabins { get; set; }

        public int CountOfMembers { get; set; }

        public override void GetDiscription()
        {
            base.GetDiscription();
            Console.WriteLine($"Name of ship: {NameOfShip}");
            Console.WriteLine($"Numbers of cabins: {NumberOfCabins}");
            Console.WriteLine($"Count of members: {CountOfMembers}");
        }
    }
}
