namespace EmployeeCompensation
{
    class PermanentEmployee : Employee
    {
        public PermanentEmployee(int eid, string ename, decimal bsal, int exp) : base(eid, ename, bsal, exp) { }

        public new decimal calculateAnnualSalary()
        {
            decimal hra = 0.2m * basicSalary;
            decimal allowance = 0.1m * basicSalary;
            decimal loyaltyBonus = (expYears >= 5) ? 50000 : 0;
            return base.calculateAnnualSalary() + hra + allowance + loyaltyBonus;
        }
    }

    class ContractEmployee : Employee
    {
        public int conDur {  get; set; }
        public ContractEmployee(int eid, string ename, decimal bsal, int exp, int dur) : base(eid, ename, bsal, exp)
        {
            conDur = dur;
        }
        
        public new decimal calculateAnnualSalary()
        {
            decimal bonus = (conDur >= 12) ? 30000 : 0;
            return base.calculateAnnualSalary() + bonus;
        }
    }

    class InternEmployee : Employee
    {
        public InternEmployee(int eid, string ename, decimal bsal, int exp) : base(eid, ename, bsal, exp) { }

        public new decimal calculateAnnualSalary()
        {
            return base.calculateAnnualSalary();
        }
    }

    internal class Employee
    {
        public int empId {  get; set; }
        public string empName { get; set; }
        public decimal basicSalary { get; set; }
        public int expYears { get; set; }
        public Employee(int eid, string ename, decimal bsal, int exp)
        {
            empId = eid;
            empName = ename;
            basicSalary = bsal;
            expYears = exp;
        }
        public decimal calculateAnnualSalary()
        {
            return basicSalary * 12;
        }
        public string displayEmp()
        {
            return $"Employee ID\t: {empId}\nEmployee Name\t: {empName}\nBasic Salary\t: {basicSalary}\nExperience\t: {expYears}\nAnnual Salary\t: {calculateAnnualSalary():F2}";
        }
        static void Main(string[] args)
        {
            Employee emp1 = new PermanentEmployee(101, "Tahmid", 30000, 6);
            PermanentEmployee emp2 = new PermanentEmployee(102, "Shreya", 30000, 6);

            Employee emp3 = new ContractEmployee(103, "Aditi", 25000, 2, 14);
            Employee emp4 = new InternEmployee(104, "Tushar", 15000, 0);

            Console.WriteLine("---- Employee Details ----\n");
            Console.WriteLine(emp1.displayEmp());
            Console.WriteLine($"In-Hand Salary\t: {emp1.calculateAnnualSalary():F2}\n");
            Console.WriteLine(emp2.displayEmp());
            Console.WriteLine($"In-Hand Salary\t: {emp2.calculateAnnualSalary():F2}\n");
            Console.WriteLine(emp3.displayEmp());
            Console.WriteLine($"In-Hand Salary\t: {emp3.calculateAnnualSalary():F2}\n");
            Console.WriteLine(emp4.displayEmp());
            Console.WriteLine($"In-Hand Salary\t: {emp4.calculateAnnualSalary():F2}\n");
        }
    }
}
