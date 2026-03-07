namespace PersonClass
{
    enum Departmant
    {
        Sales,
        Accounts,
        IT
    }
    internal class Person
    {
        private int Id;
        public void SetId(int id)
        {
            this.Id = id;
        }

        public string Name { get; set; }

        private Departmant department;

        public Departmant Departmant
        {
            get { return department; }
            set { department = value; }
        }

        private int salary;

        public int Salary
        {
            get { return salary; }
            set
            {
                if(value >= 50000 && value <= 90000)
                {
                    salary = value;
                }
                else
                {
                    Console.WriteLine("Salary must be between the Specified Range!");
                }
            }
        }

        public void Display()
        {
            Console.WriteLine($"ID\t: {Id}");
            Console.WriteLine($"Name\t: {Name}");
            Console.WriteLine($"Dept.\t: {department}");
            Console.WriteLine($"Salary\t: {salary}");
        }
        static void Main(string[] args)
        {
            Person emp = new Person();

            emp.SetId(101);
            emp.Name = "Tahmid Noor";
            emp.Departmant = Departmant.IT;
            emp.Salary = 80000;

            emp.Display();
        }
    }
}
