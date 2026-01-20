namespace OrderProcessing
{
    internal class Program
    {
        static void ReadWeeklySales(decimal[] sales)
        {
            for (int i = 0; i < sales.Length; i++)
            {
                decimal val;
                do
                {
                    Console.Write($"[{i + 1}] Enter Sales: ");
                } while (!decimal.TryParse(Console.ReadLine(), out val) || val < 0);
                sales[i] = val;
            }
        }

        static decimal CalculateTotal(decimal[] sales)
        {
            decimal total = 0;
            for (int i = 0;i < sales.Length; i++)
            {
                total += sales[i];
            }
            return total;
        }

        static decimal CalculateAverage(decimal total, int days)
        {
            return total / days;
        }

        static decimal FindHighestSale(decimal[] sales, out int day)
        {
            decimal max = sales[0];
            day = 1;

            for (int i = 1; i < sales.Length; i++)
            {
                if(sales[i] > max)
                {
                    max = sales[i];
                    day = i + 1;
                }
            }
            return max;
        }

        static decimal FindLowestSale(decimal[] sales, out int day)
        {
            decimal min = sales[0];
            day = 1;

            for(int i = 1;  i < sales.Length; i++)
            {
                if (sales[i] < min)
                {
                    min = sales[i];
                    day = i + 1;
                }
            }
            return min;
        }

        static decimal CalculateDiscount(decimal total)
        {
            return total >= 50000 ? total * 0.1m : total * 0.05m;
        }

        static decimal CalculateDiscount(decimal total, bool isFestivalWeek)
        {
            decimal disc = CalculateDiscount(total);

            if (isFestivalWeek)
            {
                disc += total * 0.05m;
            }
            return disc;
        }

        static decimal CalculateTax(decimal amount)
        {
            return amount * 0.18m;
        }

        static decimal CalculateFinalAmount(decimal total, decimal discount, decimal tax)
        {
            return total - discount + tax;
        }

        static void GenerateSalesCategory(decimal[] sales, string[] categories)
        {
            for(int i = 0; i < sales.Length; i++)
            {
                if(sales[i] < 5000)
                {
                    categories[i] = "LOW";
                }
                else if(sales[i] <= 15000)
                {
                    categories[i] = "MEDIUM";
                }
                else
                {
                    categories[i] = "HIGH";
                }
            }
        }

        static void Main(string[] args)
        {
            decimal[] weekSales = new decimal[7];
            string[] categories = new string[7];

            ReadWeeklySales(weekSales);

            decimal total = CalculateTotal(weekSales);
            decimal avg = CalculateAverage(total, 7);

            decimal maxSale = FindHighestSale(weekSales, out int maxDay);
            decimal minSale = FindLowestSale(weekSales, out int minDay);

            bool isFestivalWeek = true;

            decimal disc = CalculateDiscount(total, isFestivalWeek);
            decimal taxAmm = total - disc;
            decimal tax = CalculateTax(taxAmm);

            decimal finalAmm = CalculateFinalAmount(total, disc, tax);

            GenerateSalesCategory(weekSales, categories);

            Console.WriteLine();
            Console.WriteLine("Weekly Sales Summary");
            Console.WriteLine("--------------------");
            Console.WriteLine($"Total Sales        : {total:F2}");
            Console.WriteLine($"Average Daily Sale : {avg:F2}");
            Console.WriteLine();
            Console.WriteLine($"Highest Sale       : {maxSale:F2} (Day {maxDay})");
            Console.WriteLine($"Lowest Sale        : {minSale:F2}  (Day {minDay})");
            Console.WriteLine();
            Console.WriteLine($"Discount Applied   : {disc:F2}");
            Console.WriteLine($"Tax Amount         : {tax:F2}");
            Console.WriteLine($"Final Payable      : {finalAmm:F2}");
            Console.WriteLine();
            Console.WriteLine("Day-wise Category:");
            Console.WriteLine("------------------");
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"Day {i + 1} : {categories[i]}");
            }
        }
    }
}
