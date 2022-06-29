using System.Linq;
using ClassArchitecture.Classes;
using System;
using System.Collections.Generic;
using ClassArchitecture.Interfaces;

namespace ClassArchitecture
{
    internal class Program
    {
        static List<Transport> CreateTransport()
        {
            List<Transport> TransportList = new();
            TransportList.Add(new PowerBoat("Audi", "001", 1000, DateTime.Now, DateTime.Now, "Power boat", 8, 1, 2, "123vc11", 500));
            TransportList.Add(new CruiseShip("Band", "B123", 5000, DateTime.Now, DateTime.Now, "Cruise ship", 20, "Mary", 8, 8));
            TransportList.Add(new PassengerCar("Bmw", "x6", 100, DateTime.Now, DateTime.Now, "bb112b", 100, "B1", "Sedan", 5, 300, true, 5, "Passenger car"));
            TransportList.Add(new FreightCar("Mercedes", "007", 1500, DateTime.Now, DateTime.Now, "dsa1f", "Freight car", 1000, 500, 300, true, 5));
            TransportList.Add(new FireCar("Lada", "10", 2000, DateTime.Now, DateTime.Now, "bc123c", 400, "C", "Fire car", 1000, 4));
            TransportList.Add(new Bus("BMW", "X4", 2000, DateTime.Now, DateTime.Now, "B123vv", "Bus", 25, "B1", true, 5, true, 3, 500));
            TransportList.Add(new Trolleybus("Kia", "Rio", 2000, DateTime.Now, DateTime.Now, "123c1", "Trolleybus", 30, "B1", true, 5, true, 6, 40));
            TransportList.Add(new Motorcycle("Shevrolet", "Niva", 300, DateTime.Now, DateTime.Now, "b123vcv", "Motorcycle", 500, 5, 200));
            TransportList.Add(new Tank("Lada", "14", 4000, DateTime.Now, DateTime.Now, "bsad1", "Tank", 55, 40, 123, 5, 5, 120, 50, 50));
            TransportList.Add(new ElectricScooter("bmw", "das", 40, DateTime.Now, DateTime.Now, "123vv", "Electric Scooter", 30));
            TransportList.Add(new ElectricLocomotive("Audi", "bb", 5000, DateTime.Now, DateTime.Now, "bvcx", "Electric locomotive", 500, 300));
            TransportList.Add(new Bike("Stels", "Forward", 10, DateTime.Now, DateTime.Now, "123", "Bike", true, true, true, true, true));
            TransportList.Add(new PassengersAirplane("", "", 0, DateTime.Now, DateTime.Now, 0, 0, "0", "Passenger airplane", 123, 3, 23, 5, 12, 44));
            TransportList.Add(new Fighter("", "", 0, DateTime.Now, DateTime.Now, 0, 0, "d", "Fighter", 1, 2, 3, 4, 5, 6, 7));
            TransportList.Add(new TransportAirplane("", "", 0, DateTime.Now, DateTime.Now, 0, 1, "", "transport airplane", 1, 2, 3, 4, 5, 1, 2));
            TransportList.Add(new AttackHelicopter("", "", 1, DateTime.Now, DateTime.Now, 1, 1, "1", "Attack helicopter", 1, 1, 1, 1, 1, 1));
            TransportList.Add(new FireHelicopter("", "", 1, DateTime.Now, DateTime.Now, 1, 1, "1", "Fire helicopter", 1, 1, 1, 1, 1, 1));
            TransportList.Add(new PassengersHelicopter("", "", 1, DateTime.Now, DateTime.Now, 1, 1, "1", "Passenger helicopter", 1, 1, 1, 1, 1));
            TransportList.Add(new Airship("", "", 1, DateTime.Now, DateTime.Now, 1, 1, "1", "Airship", 1, 1, 1, 1, 1));

            return TransportList;
        }

        static void GetFullDescription(List<Transport> listOfTransport) => listOfTransport.ForEach(x => x.GetDiscription());

        static void GetMaintenance(List<Transport> listOfTransport) => listOfTransport.ForEach(x => x.GetTechInspection());
        static void StartDiagnostic(List<IEnginable> listOfTransport) => listOfTransport.ForEach(x => x.RunDiagnostics());

        static void StartAttack(List<IMilitary> listOfTransport)
        {
            foreach (var transport in listOfTransport)
            {
                transport.StartOffensive();
                while (transport.CurrentAmmo>0)
                    transport.OpenFire();
                transport.StopOffensive();
                transport.StartRetreat();
            }
        }

        static void StartCargoDelivery(List<IMovable> listOfTransport)
        {
            foreach (var freightTransport in listOfTransport)
            {
                if (freightTransport is IFreightable)
                {

                    var cargo = (IFreightable)freightTransport;
                    cargo.load();
                    freightTransport.StartMove();
                    freightTransport.StopMove();
                    cargo.unload();
                }
            }
        }

        static void StartPassengerDelivery(List<IMovable> listOfTransport)
        {
            foreach (var passengerTransport in listOfTransport)
            {
                if (passengerTransport is IPassengers)
                {
                    var pass = (IPassengers)passengerTransport;
                    pass.AcceptPassenger();
                    passengerTransport.StartMove();
                    passengerTransport.StopMove();
                    pass.DropOfPassenger();
                }
            }
        }

        static void GetFreightableWeight(List<ILoadCapacity> listOfTransport)
        {
            foreach (var passengerTransport in listOfTransport)
            {
                double cargoWeight = 0;
                if (passengerTransport is IFreightable)
                {
                    cargoWeight += passengerTransport.LoadCapacity;
                    Console.WriteLine($"LoadCapacity = {cargoWeight}");
                }
            }
        }

        static void GetPassengerCount(List<IPassengers> listOfTransport)
        {
            int passengerCount = 0;
            foreach (var passengerTransport in listOfTransport)
            {
                passengerCount += passengerTransport.NumOfPassengers;
                Console.WriteLine($"LoadCapacity = {passengerCount}");
            }
        }

        static void Main(string[] args)
        {
            List<Transport> transport = CreateTransport();

            GetFullDescription(transport);
            GetMaintenance(transport);
            StartAttack(transport.Where(x => x is IMilitary).Select(x => (IMilitary)x).ToList());
            StartDiagnostic(transport.Where(x => x is IEnginable).Select(x => (IEnginable)x).ToList());
            GetPassengerCount(transport.Where(x => x is IPassengers).Select(x => (IPassengers)x).ToList());
            StartPassengerDelivery(transport.Where(x => x is IMovable).Select(x => (IMovable)x).ToList());
            GetFreightableWeight(transport.Where(x => x is ILoadCapacity).Select(x => (ILoadCapacity)x).ToList());
            StartCargoDelivery(transport.Where(x => x is IMovable).Select(x => (IMovable)x).ToList());

            Console.Read();

        }

        
    }
}
