using System.Globalization;

namespace EcoDrive
{
    internal abstract class Vehicle
    {
        public string ModelName { get; set; }

        public abstract void Move();

        public virtual void GetFuelStatus()
        {
            Console.WriteLine("Fuel level is stable.");
        }

        static void Main(string[] args)
        {
            Vehicle[] vehicles = new Vehicle[]
            {
                new ElectricCar("Honda EV"),
                new HeavyTruck("TATA Ace"),
                new CargoPlane("Boeing B747")
            };

            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine($"[{vehicle.ModelName}]");
                vehicle.Move();
                vehicle.GetFuelStatus();
                Console.WriteLine("----------------------");
            }
        }
    }

    class ElectricCar : Vehicle
    {
        public ElectricCar(string name)
        {
            ModelName = name;
        }
        public override void Move()
        {
            Console.WriteLine($"{ModelName} is gliding silently on battery power.");
        }
        public override void GetFuelStatus()
        {
            Console.WriteLine($"{ModelName} battery is at 80%");
        }
    }

    class HeavyTruck : Vehicle
    {
        public HeavyTruck(string name)
        {
            ModelName = name;
        }
        public override void Move()
        {
            Console.WriteLine($"{ModelName} is hauling cargo with high-torque diesel power.");
        }
        public override void GetFuelStatus()
        {
            base.GetFuelStatus();
        }
    }

    class CargoPlane : Vehicle
    {
        public CargoPlane(string name)
        {
            ModelName = name;
        }
        public override void Move()
        {
            Console.WriteLine($"{ModelName} is ascending to 30,000 feet.");
        }
        public override void GetFuelStatus()
        {
            base.GetFuelStatus();
            Console.WriteLine("Checking jet fuel reserves..");
        }
    }
}
