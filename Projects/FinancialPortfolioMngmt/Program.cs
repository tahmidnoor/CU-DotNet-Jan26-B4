namespace FinancialPortfolioMngmt
{
    class InvalidFinancialDataException : Exception
    {
        public InvalidFinancialDataException(string message) : base(message) { }
    }
    interface IRiskAssessable
    {
        string GetRiskCategory();
    }

    interface IReportable
    {
        string GenerateReportLine();
    }

    abstract class FinancialInstrument
    {
        private decimal quantity;
        private decimal purchasePrice;
        public string InstrumentId { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public DateTime PurchaseDate { get; set; }

        public decimal Quantity
        {
            get => quantity;
            set
            {
                if (value < 0)
                {
                    throw new InvalidFinancialDataException("Quantity cannot be negative");
                }
                quantity = value;
            }
        }
        public decimal PurchasePrice
        {
            get => purchasePrice;
            set
            {
                if (value < 0)
                    throw new InvalidFinancialDataException("Price cannot be negative");
                purchasePrice = value;
            }
        }
        public decimal MarketPrice { get; set; }
        public abstract decimal CalculateCurrentValue();

        public virtual string GetInstrumentSummary()
        {
            return $"{InstrumentId}-{Name} ({Currency}";
        }

    }
    class Equity : FinancialInstrument, IRiskAssessable, IReportable
    {
        public override decimal CalculateCurrentValue()
        {
            return Quantity * MarketPrice;
        }
        public string GetRiskCategory() => "High";
        public string GenerateReportLine()
        {
            return $"{InstrumentId} | Equity | Value: {CalculateCurrentValue():C}";
        }
    }
    class Bond : FinancialInstrument, IRiskAssessable, IReportable
    {
        public override decimal CalculateCurrentValue()
        {
            return Quantity * MarketPrice;
        }
        public string GetRiskCategory() => "Low";
        public string GenerateReportLine()
        {
            return $"{InstrumentId}|Bond | Value: {CalculateCurrentValue():C}";
        }
    }

    class FixedDeposit : FinancialInstrument
    {
        public override decimal CalculateCurrentValue()
        {
            return Quantity * MarketPrice;
        }
    }

    class MutualFund : FinancialInstrument
    {
        public override decimal CalculateCurrentValue()
        {
            return Quantity * MarketPrice;
        }

    }
    class Portfolio
    {
        private List<FinancialInstrument> instruments = new List<FinancialInstrument>();
        private Dictionary<string, FinancialInstrument> lookup = new Dictionary<string, FinancialInstrument>();
        public void AddInstrument(FinancialInstrument instrument)
        {
            if (lookup.ContainsKey(instrument.InstrumentId))
            {
                throw new Exception("Duplicate Instrument ID");
            }
            instruments.Add(instrument);
            lookup[instrument.InstrumentId] = instrument;
        }
        public void RemoveInstrument(string id)
        {
            if (lookup.ContainsKey(id))
            {
                instruments.Remove(lookup[id]);
                lookup.Remove(id);
            }
        }
        public decimal GetTotalPortfolioValue()
        {
            return instruments.Sum(i => i.CalculateCurrentValue());
        }
        public FinancialInstrument GetInstrumentById(string id)
        {
            return lookup.ContainsKey(id) ? lookup[id] : null;
        }
        public IEnumerable<FinancialInstrument> GetInstrumentsByRisk(string risk)
        {
            return instruments.OfType<IRiskAssessable>().Where(i => i.GetRiskCategory() == risk).Cast<FinancialInstrument>();
        }
        public List<FinancialInstrument> GetAll() => instruments;


    }
    class Transaction
    {
        public string TransactionId { get; set; }
        public string InstrumentId { get; set; }
        public string Type { get; set; }
        public decimal Units { get; set; }
        public DateTime Date { get; set; }
    }
    class ReportGenerator
    {
        public static void GenerateConsoleReport(Portfolio portfolio)
        {
            Console.WriteLine("=====PORTFOLIO SUMMARY======");
            var grouped = portfolio.GetAll().GroupBy(i => i.GetType().Name);
            foreach (var group in grouped)
            {
                decimal investment = group.Sum(i => i.PurchasePrice * i.Quantity);
                decimal current = group.Sum(i => i.CalculateCurrentValue());

                Console.WriteLine($"\nInstrument type: {group.Key}");
                Console.WriteLine($"\ntotal Instrument : {investment:C}");
                Console.WriteLine($"\nCurrent Value : {current:C}");
                Console.WriteLine($"\nProfit/Loss: {(current - investment)}");

            }
            Console.WriteLine($"Overall Portfolio Value: {portfolio.GetTotalPortfolioValue():C}");
        }
        public static void GenerateFileReport(Portfolio portfolio)
        {
            string fileName = $"PortfolioReport_{DateTime.Now:yyyyMMdd}.txt";

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("PORTFOLIO REPORT");
                sw.WriteLine("Generated: " + DateTime.Now);

                foreach (var i in portfolio.GetAll())
                {
                    sw.WriteLine($"{i.GetInstrumentSummary()} | Value: {i.CalculateCurrentValue():C}");
                }

                sw.WriteLine("\nTotal Value: " + portfolio.GetTotalPortfolioValue().ToString("C"));
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Portfolio portfolio = new Portfolio();

            try
            {
                string csv = "EQ001,Equity,INFY,INR,100,1500,1650";
                var parts = csv.Split(',');

                FinancialInstrument eq = new Equity
                {
                    InstrumentId = parts[0],
                    Name = parts[2],
                    Currency = parts[3],
                    Quantity = decimal.Parse(parts[4]),
                    PurchasePrice = decimal.Parse(parts[5]),
                    MarketPrice = decimal.Parse(parts[6]),
                    PurchaseDate = DateTime.Now
                };

                portfolio.AddInstrument(eq);

                FinancialInstrument bond = new Bond
                {
                    InstrumentId = "BD001",
                    Name = "Gov Bond",
                    Currency = "INR",
                    Quantity = 50,
                    PurchasePrice = 1000,
                    MarketPrice = 1050,
                    PurchaseDate = DateTime.Now
                };

                portfolio.AddInstrument(bond);

                ReportGenerator.GenerateConsoleReport(portfolio);


                ReportGenerator.GenerateFileReport(portfolio);
            }
            catch (InvalidFinancialDataException ex)
            {
                Console.WriteLine("Data Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}