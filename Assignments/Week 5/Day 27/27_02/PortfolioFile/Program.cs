namespace PortfolioFile
{
    public class Loan
    {
        public string ClientName { get; set; }
        public double Principal { get; set; }
        public double InterestRate { get; set; }

    }
    internal class Program
    {
        static void AddLoan()
        {
            bool fileExists = File.Exists(filePath);
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                if (!fileExists)
                {
                    sw.WriteLine("ClientName, Principal, InterestRate");
                }
                Console.Write("Enter Client Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Principal Amount: ");
                double principal = double.Parse(Console.ReadLine());
                Console.Write("Enter Interest Rate (%): ");
                double rate = double.Parse(Console.ReadLine());

                sw.WriteLine($"{name},{principal},{rate}");
            }
        }
        static string GetRiskLevel(double rate)
        {
            if (rate > 10)
            {
                return "High Risk";
            }
            else if (rate >= 5)
            {
                return "Medium Risk";
            }
            else
            {
                return "Low Risk";
            }
        }
        static void ReadLoans()
        {
            Console.WriteLine(
                $"{"CLIENT",-18} | {"PRINCIPAL",-15} | {"INTEREST",-12} | {"RISK LEVEL",-10}"
            );
            Console.WriteLine(new string('-', 65));
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    if (!double.TryParse(data[1], out double principal) || !double.TryParse(data[2], out double rate))
                    {
                        Console.WriteLine("Invalid record found. Skipping...");
                        continue;
                    }
                    double interestAmount = principal * rate / 100;
                    string risk = GetRiskLevel(rate);
                    Console.WriteLine(
                        $"{data[0],-18} | " +
                        $"{principal,15:C} | " +
                        $"{interestAmount,12:C} | " +
                        $"{risk,-10}"
                    );
                }
            }
        }

        static string file = "loans.csv";
        static string directory = @"..\..\..\";
        static string filePath = Path.Combine(directory, file);
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            AddLoan();
            Console.WriteLine("\n-----Loan Portfolio------");
            Console.WriteLine();
            ReadLoans();
        }
    }
}
