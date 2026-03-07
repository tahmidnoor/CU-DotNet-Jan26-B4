namespace Loan
{
    internal class Loann
    {
        public string loanNo {  get; set; }
        public string customerName { get; set; }
        public decimal principalAmmount { get; set; }
        public int years { get; set; }

        public Loann()
        {
            loanNo = "L1";
            customerName = string.Empty;
            principalAmmount = 0;
            years = 0;
        } 
        public Loann(string lnum, string cname, decimal pamm, int tiy)
        {
            loanNo = lnum;
            customerName = cname;
            principalAmmount = pamm;
            years = tiy;
        }

        public double calculateEMI()
        {
            double SI = (double)(principalAmmount * 10 * years) / 100;
            return SI;
        }
        static void Main(string[] args)
        {
            Loann[] loans = new Loann[4]
            {
                new HomeLoan("HL1", "Tahmid Noor", 700000, 10),
                new HomeLoan("HL2", "Shreya Biswas", 500000, 8),
                new CarLoan("CL1", "Aditi Chaturvedi", 800000, 5),
                new CarLoan("CL2", "Shreyansh Vishnoi", 400000, 4)
            };

            foreach (var loan in loans)
            {
                Console.WriteLine($"[{loan.loanNo}] EMI: {loan.calculateEMI()}");
            }
        }
    }

    class HomeLoan : Loann
    {
        public HomeLoan() { }

        public HomeLoan(string lnum, string cname, decimal pamm, int tiy) : base(lnum, cname, pamm, tiy) { }

        public new double calculateEMI()
        {
            decimal processingFee = principalAmmount / 100;
            double interest = (double)(principalAmmount * 8 * years) / 100;
            return interest + (double)processingFee;
        }
    }

    class CarLoan : Loann
    {
        public CarLoan() { }

        public CarLoan(string lnum, string cname, decimal pamm, int tiy) : base(lnum, cname, pamm, tiy) { }

        public new double calculateEMI()
        {
            principalAmmount += 15000;
            double interest = (double)(principalAmmount * 9 * years) / 100;
            return interest;
        }
    }
}
