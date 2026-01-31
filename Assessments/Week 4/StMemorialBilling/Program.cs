namespace StMemorialBilling
{
    public class Patient
    {
        public string Name { get; set; }
        public decimal BaseFee { get; set; }

        public Patient(string name, decimal baseFee)
        {
            Name = name;
            BaseFee = baseFee;
        }

        public virtual decimal calculateBill()
        {
            return BaseFee;
        }
    }

    public class Inpatient : Patient
    {
        public int DaysStayed { get; set; }
        public decimal DailyRate { get; set; }

        public Inpatient(string name, decimal baseFee, int daysStayed, decimal dailyRate) : base(name, baseFee)
        {
            DaysStayed = daysStayed;
            DailyRate = dailyRate;
        }

        public override decimal calculateBill()
        {
            return BaseFee + (DaysStayed *  DailyRate);
        }
    }

    public class Outpatient : Patient
    {
        public decimal ProcedureFee { get; set; }

        public Outpatient(string name, decimal baseFee, decimal procedureFee) : base(name, baseFee)
        {
            ProcedureFee = procedureFee;
        }

        public override decimal calculateBill()
        {
            return BaseFee + ProcedureFee;
        }
    }

    public class EmergencyPatient : Patient
    {
        public int SeverityLevel { get; set; }

        public EmergencyPatient(string name, decimal baseFee, int severityLevel) : base(name, baseFee)
        {
            SeverityLevel = severityLevel;
        }

        public override decimal calculateBill()
        {
            return BaseFee * SeverityLevel;
        }
    }

    public class HospitalBilling
    {
        private List<Patient> patients = new List<Patient>();

        public void AddPatient(Patient patient)
        {
            patients.Add(patient);
        }

        public void GenerateDailyReport()
        {
            foreach (Patient patient in patients)
            {
                decimal bill = patient.calculateBill();
                Console.WriteLine($"{patient.Name}\t: {bill.ToString("C2")}");
            }
        }

        public decimal CalculateTotalRevenue()
        {
            decimal total = 0;
            foreach (Patient patient in patients)
            {
                total += patient.calculateBill();
            }
            return total;
        }

        public int GetInpatientCount()
        {
            int cnt = 0;
            foreach(Patient patient in patients)
            {
                if(patient is Inpatient)
                {
                    cnt++;
                }
            }
            return cnt;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            HospitalBilling bills = new HospitalBilling();

            bills.AddPatient(new Inpatient("Tahmid", 5000m, 3, 500m));
            bills.AddPatient(new Inpatient("Shreya", 4000m, 2, 350m));
            bills.AddPatient(new Outpatient("Tushar", 7000m, 800m));
            bills.AddPatient(new EmergencyPatient("Aditi", 3000m, 3));
            bills.AddPatient(new Inpatient("Shivam", 3500m, 6, 250m));

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("--- Daily Report ---");
            bills.GenerateDailyReport();
            Console.WriteLine();
            Console.WriteLine($"Total Revenue: {bills.CalculateTotalRevenue().ToString("C2")}");
            Console.WriteLine($"Inpatient Count: {bills.GetInpatientCount()}");
        }
    }
}
