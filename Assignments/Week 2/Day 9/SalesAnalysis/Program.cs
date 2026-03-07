namespace SalesAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal[] dSales = new decimal[7];
            string[] sCatg = new string[7];

            int days = dSales.Length;

            for (int i = 0; i < days; i++)
            {
                while (true)
                {
                    Console.Write($"[Day {i+1}] Enter Sales: ");

                    if (decimal.TryParse(Console.ReadLine(), out decimal sale) && sale >= 0)
                    {
                        dSales[i] = sale;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Sales must be a non-negative number.");
                    }
                }
            }

            decimal tSales = 0;

            for (int i = 0; i < days; i++)
            {
                tSales += dSales[i];
            }

            decimal avgSales = tSales / days;

            decimal maxSale = dSales[0];
            decimal minSale = dSales[0];
            int maxDay = 1;
            int minDay = 1;

            for (int i = 1; i < days; i++)
            {
                if (dSales[i] > maxSale)
                {
                    maxSale = dSales[i];
                    maxDay = i + 1;
                }

                if (dSales[i] < minSale)
                {
                    minSale = dSales[i];
                    minDay = i + 1;
                }
            }

            int daysAboveAvg = 0;

            for (int i = 0; i < days; i++)
            {
                if (dSales[i] > avgSales)
                {
                    daysAboveAvg++;
                }

                if (dSales[i] < 5000)
                {
                    sCatg[i] = "Low";
                }
                else if (dSales[i] <= 15000)
                {
                    sCatg[i] = "Medium";
                }
                else
                {
                    sCatg[i] = "High";
                }
            }

            Console.WriteLine();
            Console.WriteLine("Weekly Sales Report");
            Console.WriteLine("-------------------");
            Console.WriteLine($"Total Sales\t\t: {tSales:F2}");
            Console.WriteLine($"Average Daily Sale\t: {avgSales:F2}");
            Console.WriteLine($"Highest Sale\t\t: {maxSale:F2} (Day {maxDay})");
            Console.WriteLine($"Lowest Sale\t\t: {minSale:F2} (Day {minDay})");
            Console.WriteLine();
            Console.WriteLine($"Days Above Average\t: {daysAboveAvg}");
            Console.WriteLine();
            Console.WriteLine("Day-wise Sales Category");
            Console.WriteLine("-----------------------");

            for (int i = 0; i < days; i++)
            {
                Console.WriteLine($"Day {i + 1} : {sCatg[i]}");
            }
        }
    }
}



