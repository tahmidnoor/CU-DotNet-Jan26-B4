namespace InsuranceSummary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string[5];
            decimal[] prem = new decimal[5];

            decimal tPrem = 0;

            for (int i = 0; i < 5; i++)
            {
                string name;
                do
                {
                    Console.Write($"[{i + 1}] Enter Name of Policyholder: ");
                    name = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(name));
                names[i] = name;

                decimal premium;
                do
                {
                    Console.Write($"    Enter Annual Premium: ");
                } while (!decimal.TryParse(Console.ReadLine(), out premium) || premium <= 0);
                prem[i] = premium;

                tPrem += premium;
                Console.WriteLine();
            }

            decimal maxPrem = prem.Max();
            decimal minPrem = prem.Min();
            decimal avgPrem = prem.Average();

            string[] catg = new string[5];
            for(int i = 0; i<5; i++)
            {
                if (prem[i] > 25000)
                {
                    catg[i] = "HIGH";
                }
                else if(prem[i] > 10000)
                {
                    catg[i] = "MEDIUM";
                }
                else
                {
                    catg[i] = "LOW";
                }
            }
            Console.WriteLine();
            Console.WriteLine("Insurance Premium Summary");
            Console.WriteLine("-------------------------");
            Console.WriteLine();
            Console.WriteLine("+-----------------+-----------------------+-----------------+");
            Console.WriteLine("| {0,-15} |  {1,-20} |  {2,-15}|", "Name", "Premium", "Category");
            Console.WriteLine("+-----------------+-----------------------+-----------------+");
            for (int i = 0; i<5; i++)
            {
                Console.WriteLine($"| {names[i].ToUpper(), -15} |  {prem[i],-20:F2} |  {catg[i],-15}|");
            }
            Console.WriteLine("+-----------------+-----------------------+-----------------+");
            Console.WriteLine($"Total Premium\t: {tPrem:F2}");
            Console.WriteLine($"Average Premium\t: {avgPrem:F2}");
            Console.WriteLine($"Highest Premium\t: {maxPrem:F2}");
            Console.WriteLine($"Lowest Premium\t: {minPrem:F2}");
        }
    }
}