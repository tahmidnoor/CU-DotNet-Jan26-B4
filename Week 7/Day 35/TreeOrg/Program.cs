namespace TreeOrg
{
    public class EmployeeNode
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public List<EmployeeNode> Reports { get; set; } = new List<EmployeeNode>();

        public EmployeeNode(string name, string position)
        {
            Name = name;
            Position = position;
            Reports = new List<EmployeeNode>();
        }

        public void AddReport(EmployeeNode employee)
        {
            Reports.Add(employee);
        }
    }
    public class OrganizationTree
    {
        public EmployeeNode Root { get; private set; }

        public OrganizationTree(EmployeeNode rootEmployee)
        {
            Root = rootEmployee;
        }
        public void Display()
        {
            if (Root == null) return;
            Console.WriteLine("ORGANIZATION STRUCTURE");
            Console.WriteLine("----------------------");
            PrintRecursive(Root, 0);
        }

        private void PrintRecursive(EmployeeNode current, int depth)
        {
            string indent = new string(' ', depth * 4);
            string connector = depth == 0 ? "" : "└── ";

            Console.WriteLine($"{indent}{connector}{current.Name} ({current.Position})");
            foreach (var report in current.Reports)
            {
                PrintRecursive(report, depth + 1);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var ceo = new EmployeeNode("Aman", "CEO");
            var cto = new EmployeeNode("Suresh", "CTO");
            var manager = new EmployeeNode("Sonia", "Dev Manager");
            var dev1 = new EmployeeNode("Sara", "Senior Dev");
            var dev2 = new EmployeeNode("Divakar", "Junior Dev");
            var cfo = new EmployeeNode("Rajesh", "CFO");
            var acccOfficer = new EmployeeNode("Rajat", "Account Officer");

            var company = new OrganizationTree(ceo);

            ceo.AddReport(cto);

            cto.AddReport(manager);
            manager.AddReport(dev1);
            manager.AddReport(dev2);

            ceo.AddReport(cfo);
            cfo.AddReport(acccOfficer);

            company.Display();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
