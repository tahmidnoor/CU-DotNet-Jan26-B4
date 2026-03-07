namespace OLASystem
{
    class Ride
    {
        public int RideID {  get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public double Fare { get; set; }

    }

    class OLADriver
    {
        public int DriverID { get; set; }
        public string DriverName { get; set; }
        public string VehicleNo { get; set; }
        public List<Ride> Rides { get; set; }

        public OLADriver()
        {
            Rides = new List<Ride>();
        }

        public void AddRide(Ride ride)
        {
            Rides.Add(ride);
        }

        public double TotalFare()
        {
            double total = 0;
            foreach (Ride r in Rides)
            {
                total += r.Fare;
            }
            return total;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<OLADriver> drivers = new List<OLADriver>();

            OLADriver d1 = new OLADriver
            {
                DriverID = 1,
                DriverName = "Tahmid",
                VehicleNo = "MH12KJ5621"
            };

            d1.AddRide(new Ride { RideID = 101, From = "Kharar", To = "Mohali", Fare = 250 });
            d1.AddRide(new Ride { RideID = 102, From = "Chandigarh", To = "Panchkula", Fare = 750 });

            OLADriver d2 = new OLADriver
            {
                DriverID = 2,
                DriverName = "Tushar",
                VehicleNo = "DL3CSN8943"
            };

            d2.AddRide(new Ride { RideID = 201, From = "Delhi", To = "Noida", Fare = 300 });
            d2.AddRide(new Ride { RideID = 202, From = "Noida", To = "Gurgaon", Fare = 450 });
            d2.AddRide(new Ride { RideID = 203, From = "Rohini", To = "Badli", Fare = 150 });

            drivers.Add(d1);
            drivers.Add(d2);

            Console.WriteLine("Drivers Details:");
            foreach (OLADriver driver in drivers)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine($"Driver ID\t: {driver.DriverID}");
                Console.WriteLine($"Driver Name\t: {driver.DriverName}");
                Console.WriteLine($"Vehicle No\t: {driver.VehicleNo}");
                Console.WriteLine("Rides:");

                foreach (Ride r in driver.Rides)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        $" RideID\t: {r.RideID}\n From\t: {r.From}\n To\t: {r.To}\n Fare\t: {r.Fare}"
                    );
                }
                Console.WriteLine();
                Console.WriteLine($"Total Fare Earned: Rs. {driver.TotalFare()}");
            }
        }
    }
}
