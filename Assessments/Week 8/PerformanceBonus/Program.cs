namespace PerformanceBonus
{
    public class EmployeeBonus
    {
        public decimal BaseSalary { get; set; }
        public int PerformanceRating { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal DepartmentMultiplier { get; set; }
        public double AttendancePercentage { get; set; }

        public decimal NetAnnualBonus
        {
            get
            {
                if(BaseSalary <= 0)
                {
                    return 0m;
                }
                if(PerformanceRating < 1 || PerformanceRating > 5)
                {
                    throw new ArgumentOutOfRangeException("Invalid Performance Rating!");
                }
                if(AttendancePercentage < 0 || AttendancePercentage > 100)
                {
                    throw new ArgumentOutOfRangeException("Invalid Attendance Percentage!");
                }

                decimal bonus = 0m;

                switch (PerformanceRating)
                {
                    case 5:
                        bonus = 0.25m;
                        break;

                    case 4:
                        bonus = 0.18m;
                        break;

                    case 3:
                        bonus = 0.12m;
                        break;

                    case 2:
                        bonus = 0.05m;
                        break;

                    case 1:
                        bonus = 0m;
                        break;
                }

                if(YearsOfExperience > 10)
                {
                    bonus += 0.05m;
                }
                else if(YearsOfExperience > 5)
                {
                    bonus += 0.03m;
                }

                decimal finalBonus = BaseSalary * bonus;

                if(AttendancePercentage < 85)
                {
                    finalBonus = finalBonus * 0.8m;
                }

                finalBonus = finalBonus * DepartmentMultiplier;

                decimal maxCap = BaseSalary * 0.4m;

                if(finalBonus > maxCap)
                {
                    finalBonus = maxCap;
                }

                decimal taxRate = 0m;

                if(finalBonus <= 150000m)
                {
                    taxRate = 0.1m;
                }
                else if(finalBonus <= 300000m)
                {
                    taxRate = 0.2m;
                }
                else
                {
                    taxRate = 0.3m;
                }
                decimal annualBonus = finalBonus - (finalBonus * taxRate);

                return Math.Round(annualBonus, 2);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Annual Performance Bonus System!");
        }
    }
}
