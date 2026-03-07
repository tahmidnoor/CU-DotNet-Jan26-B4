namespace AeroCook
{
    public interface ITimer
    {
        void SetTimer(int min);
    }

    public interface ISmart
    {
        void ConnectWifi(string id);
    }

    public abstract class KitchenAppliances
    {
        public string ModelName { get; set; }
        public int PowerConsumption { get; set; }

        public KitchenAppliances(string modelName, int powerConsumption)
        {
            ModelName = modelName;
            PowerConsumption = powerConsumption;
        }

        public virtual void PreHeat()
        {
            Console.WriteLine("No Preheat!");
        }

        public abstract void Cook();
    }

    public class Microwave : KitchenAppliances, ITimer, ISmart
    {
        public Microwave(string modelName, int powerConsumption): base(modelName, powerConsumption) { }

        public void SetTimer(int min)
        {
            Console.WriteLine($"Timer set in Microwave for {min} minutes..");
        }
        

        public void ConnectWifi(string id)
        {
            Console.WriteLine($"Connected to ID: {id}");
        }

        public override void Cook()
        {
            Console.WriteLine("Heating Food in Microwave!");
        }
    }

    public class Oven : KitchenAppliances
    {
        public Oven(string modelName, int powerConsumption) : base(modelName, powerConsumption) { }

        public override void PreHeat()
        {
            Console.WriteLine("Preheating Oven..");
        }

        public override void Cook()
        {
            Console.WriteLine("Cooking Food on Oven!");
        }
    }

    public class AirFryer : KitchenAppliances, ITimer
    {
        public AirFryer(string modelName, int powerConsumption) : base(modelName, powerConsumption) { }

        public void SetTimer(int min)
        {
            Console.WriteLine($"Timer set in AirFryer for {min} minutes..");
        }

        public override void Cook()
        {
            Console.WriteLine("Frying Food with Air!");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<KitchenAppliances> appliances = new List<KitchenAppliances>
            {
                new Microwave("LG Solo", 1200),
                new Oven("Bajaj Infrared", 2400),
                new AirFryer("Pigeon Healthifry", 1500)
            };

            foreach(var appliance in appliances)
            {
                Console.WriteLine(appliance);
                if(appliance is ISmart)
                {
                    ISmart smart = (ISmart)appliance;
                    smart.ConnectWifi("TnHome");
                }
                if(appliance is ITimer)
                {
                    ITimer timer = (ITimer)appliance;
                    timer.SetTimer(10);
                }
                appliance.PreHeat();
                appliance.Cook();
                Console.WriteLine("-------------------");
            }
        }
    }
}
