namespace GlobalFreightTrackingSystem
{
    public class RestrictedDestinationException : Exception
    {
        public string DeniedLocation { get; }
        public RestrictedDestinationException(string location) : base($"{location} is a Restricted Location!")
        {
            DeniedLocation = location;
        }
    }

    public class InsecurePackagingException : Exception
    {
        public InsecurePackagingException(string message) : base(message) { }
    }

    public abstract class Shipment
    {
        public List<string> RestrictedZones = new List<string> { "North Pole", "Unknown Island" };

        public string TrackingId { get; set; }
        public double Weight { get; set; }
        public string Destination {  get; set; }
        public bool Fragile { get; set; }
        public bool Reinforced { get; set; }


        public Shipment(string trackingId, double weight, string destination, bool fragile, bool reinforced)
        {
            TrackingId = trackingId;
            Weight = weight;
            Destination = destination;
            Fragile = fragile;
            Reinforced = reinforced;
        }
        public abstract void ProcessShipment();
    }

    public class ExpressShipment : Shipment
    {
        public ExpressShipment(string id, double weight, string destination, bool fragile, bool reinforced) : base(id, weight, destination, fragile, reinforced){ }

        public override void ProcessShipment()
        {
            if(Weight <= 0)
            {
                throw new ArgumentOutOfRangeException("Weight must be greater than 0!");
            }
            if (RestrictedZones.Contains(Destination))
            {
                throw new RestrictedDestinationException(Destination);
            }
            if(Fragile && !Reinforced)
            {
                throw new InsecurePackagingException("Fragile Shipment not Reinforced!");
            }

            Console.WriteLine($"[{TrackingId}] Processed Successfully!");
        }
    }

    public class HeavyFreight : Shipment
    {
        public bool HLPermit { get; set; }

        public HeavyFreight(string id, double weight, string destination, bool fragile, bool reinforced, bool permit) : base(id, weight, destination, fragile, reinforced)
        {
            HLPermit = permit;
        }

        public override void ProcessShipment()
        {
            if (Weight <= 0)
            {
                throw new ArgumentOutOfRangeException("Weight must be greater than 0!");
            }
            if (RestrictedZones.Contains(Destination))
            {
                throw new RestrictedDestinationException(Destination);
            }
            if (Fragile && !Reinforced)
            {
                throw new InsecurePackagingException("Fragile Shipment not Reinforced!");
            }
            if (Weight > 1000 && !HLPermit)
            {
                throw new Exception("Heavy Lift Shipment requires Permit!");
            }
            Console.WriteLine($"[{TrackingId}] Processed Successfully!");
        }
    }
    public interface ILoggable
    {
        void SaveLog(string message);
    }

    public class Logger : ILoggable
    {
        string path = "../../../shipment_audit.log";
        public void SaveLog(string message)
        {
            using StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine($"[{DateTime.Now}] {message}");
        }
    }
   
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger lg = new Logger();
            List<Shipment> shipments = new List<Shipment>
            {
                new ExpressShipment("E101", 8, "Kolkata", true, false),
                new ExpressShipment("E102", -4, "Mumbai", false, false),
                new ExpressShipment("E103", 15, "London", true, true),
                new HeavyFreight("H101", 1500, "Delhi", true, true, false),
                new HeavyFreight("H102", 1400, "North Pole", false, true, true),
                new HeavyFreight("H103", 1200, "Chennai", true, true, true)
            };

            foreach(Shipment shipment in shipments)
            {
                try
                {
                    shipment.ProcessShipment();
                    lg.SaveLog($"SUCCESS: {shipment.TrackingId}");
                }
                catch (RestrictedDestinationException ex)
                {
                    lg.SaveLog($"ALERT: {ex.Message}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    lg.SaveLog($"ERROR: {ex.Message}");
                }
                catch (Exception ex)
                {
                    lg.SaveLog($"ERROR: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine($"Processing Ended!");
                }
            }
        }
    }
}
