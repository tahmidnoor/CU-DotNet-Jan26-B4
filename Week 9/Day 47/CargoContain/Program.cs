namespace CargoContain
{
    public class Item
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Category { get; set; }
        public Item(string name, double wt, string ctg)
        {
            Name = name;
            Weight = wt;
            Category = ctg;
        }

    }
    public class Container
    {
        public string ContainerID { get; set; }
        public List<Item> Items { get; set; }

        public Container(string CID, List<Item> items)
        {
            ContainerID = CID;
            Items = items ?? new List<Item>();
        }


    }
    public class CargoManager
    {
        public List<List<Container>> cargoBay;
        public CargoManager(List<List<Container>> cargoBay)
        {
            this.cargoBay = cargoBay ?? new List<List<Container>>();
        }
        public List<string> FindHeavyContainers(double weightThreshold)
        {
            List<string> ans = new List<string>();

            foreach (var row in cargoBay)
            {
                if (row == null) continue;

                foreach (var container in row)
                {
                    if (container?.Items == null) continue;

                    double sum = 0;

                    foreach (var item in container.Items)
                    {
                        sum += item.Weight;
                    }

                    if (sum > weightThreshold)
                    {
                        ans.Add(container.ContainerID);
                    }
                }
            }

            return ans;
        }


        public Dictionary<string, int> GetItemCountsByCategory()
        {
            return cargoBay
                .Where(row => row != null)
                .SelectMany(row => row)
                .Where(container => container?.Items != null)
                .SelectMany(container => container.Items)
                .GroupBy(item => item.Category)
                .ToDictionary(g => g.Key, g => g.Count());
        }


        public List<Item> FlattenAndSortShipment()
        {
            return cargoBay
                .Where(row => row != null)
                .SelectMany(row => row)
                .Where(container => container?.Items != null)
                .SelectMany(container => container.Items)
                .Where(item => item != null)
                .DistinctBy(item => item.Name)
                .OrderBy(item => item.Category)
                .ThenByDescending(item => item.Weight)
                .ToList();
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {

            var cargoBay = new List<List<Container>>
            {
                new List<Container>
                {
                    new Container("C001", new List<Item>
                    {
                        new Item("Laptop", 2.5, "Tech"),
                        new Item("Monitor", 5.0, "Tech"),
                        new Item("Smartphone", 0.5, "Tech")
                    }),
                    new Container("C104", new List<Item>
                    {
                        new Item("Server Rack", 45.0, "Tech"),
                        new Item("Cables", 1.2, "Tech")
                    })
                },

                new List<Container>
                {
                    new Container("C002", new List<Item>
                    {
                        new Item("Apple", 0.2, "Food"),
                        new Item("Banana", 0.2, "Food"),
                        new Item("Milk", 1.0, "Food")
                    }),
                    new Container("C003", new List<Item>
                    {
                        new Item("Table", 15.0, "Furniture"),
                        new Item("Chair", 7.5, "Furniture")
                    })
                },


                new List<Container>
                {
                    new Container("C205", new List<Item>
                    {
                        new Item("Vase", 3.0, "Decor"),
                        new Item("Mirror", 12.0, "Decor")
                    }),
                    new Container("C206", new List<Item>())
                    },
                new List<Container>()
            };
            CargoManager manager = new CargoManager(cargoBay);
            Console.WriteLine("Heavy Containers (> 20 weight):");
            double weightThreshold = 20;
            List<string> heavyContainers = manager.FindHeavyContainers(weightThreshold);

            foreach (string container in heavyContainers)
            {
                Console.WriteLine(container);
            }

            Console.WriteLine("\nItem Count By Category:");
            var categoryCounts = manager.GetItemCountsByCategory();
            foreach (var entry in categoryCounts)
            {
                Console.WriteLine($"{entry.Key} : {entry.Value}");
            }
        }
    }
}