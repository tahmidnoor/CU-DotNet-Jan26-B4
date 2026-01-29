namespace TransactionAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Transaction Details: ");
            string input = Console.ReadLine();
            string[] parts = input.Split("#");
            
            string tID = parts[0];
            string name = parts[1];
            string tNarr = parts[2];

            string Narr = tNarr.Trim().ToLower();

            while(Narr.Contains("  "))
            {
                Narr.Replace("  ", " ");
            }

            bool dep = Narr.Contains("deposit");
            bool wit = Narr.Contains("withdrawal");
            bool tra = Narr.Contains("transfer");

            string cat = string.Empty;

            if(!(dep || wit || tra))
            {
                cat = "NON-FINANCIAL TRANSACTION!";
            }
            else if(Narr.Equals("cash deposit successful"))
            {
                cat = "STANDARD TRANSACTION!";
            }
            else
            {
                cat = "CUSTOM TRANSACTION!";
            }

            Console.WriteLine($"Transaction ID\t: {tID}");
            Console.WriteLine($"Account Holder\t: {name}");
            Console.WriteLine($"Narration\t: {Narr}");
            Console.WriteLine($"Category\t: {cat}");
        }
    }
}
