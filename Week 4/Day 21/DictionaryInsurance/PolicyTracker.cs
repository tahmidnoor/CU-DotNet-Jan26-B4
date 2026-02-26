namespace DictionaryInsurance
{
    class Policy
    {
        public string HolderName { get; set; }
        public decimal Premium { get; set; }
        public int RiskScore { get; set; }
        public DateTime RenewalDate { get; set; }

        public Policy(string holderName, decimal premium, int riskScore, DateTime renewalDate)
        {
            HolderName = holderName;
            Premium = premium;
            RiskScore = riskScore;
            RenewalDate = renewalDate;
        }
    }
    internal class PolicyTracker
    {
        Dictionary<string, Policy> policies = new Dictionary<string, Policy>();
        public void AddPolicy(string policyId, Policy policy)
        {
            policies[policyId] = policy;
        }
        public void BulkAdjustment()
        {
            foreach (var item in policies.Values)
            {
                if (item.RiskScore > 75)
                {
                    item.Premium += item.Premium * 0.05m;
                }
            }

        }
        public void CleanUp()
        {
            List<string> keysToRemove = new List<string>();
            foreach (var pair in policies)
            {
                if (pair.Value.RenewalDate < DateTime.Now.AddYears(-3))
                {
                    keysToRemove.Add(pair.Key);
                }

            }
            foreach (var key in keysToRemove)
            {
                policies.Remove(key);
            }
        }
        public void GetPolicyById(string policyId)
        {
            if (policies.TryGetValue(policyId, out Policy policy))
            {
                Console.WriteLine("Policy Found:");
                Console.WriteLine($"Holder Name : {policy.HolderName}");
                Console.WriteLine($"Premium     : {policy.Premium}");
                Console.WriteLine($"Risk Score  : {policy.RiskScore}");
                Console.WriteLine($"Renewal Date: {policy.RenewalDate.ToShortDateString()}");
            }
            else
            {
                Console.WriteLine("Policy Not Found");
            }
        }
        public void DisplayAll()
        {
            foreach (var pair in policies)
            {
                Console.WriteLine($"ID: {pair.Key}, Holder: {pair.Value.HolderName}, Premium: {pair.Value.Premium}");
            }
        }
        static void Main(string[] args)
        {
            PolicyTracker tracker = new PolicyTracker();


            tracker.AddPolicy("P101", new Policy("Amit Sharma", 12000m, 80, DateTime.Now.AddYears(-1)));
            tracker.AddPolicy("P102", new Policy("Neha Verma", 15000m, 60, DateTime.Now.AddYears(-4)));
            tracker.AddPolicy("P103", new Policy("Rahul Singh", 18000m, 90, DateTime.Now.AddMonths(-6)));

            Console.WriteLine("---- All Policies ----");
            tracker.DisplayAll();

            Console.WriteLine("\n---- Bulk Adjustment (RiskScore > 75) ----");
            tracker.BulkAdjustment();
            tracker.DisplayAll();

            Console.WriteLine("\n---- Clean-Up (Older than 3 years) ----");
            tracker.CleanUp();
            tracker.DisplayAll();

            Console.WriteLine("\n---- Security Check ----");
            tracker.GetPolicyById("P103");
            tracker.GetPolicyById("P999");

            Console.ReadLine();
        }

    }
}
