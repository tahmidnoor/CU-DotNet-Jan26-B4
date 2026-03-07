namespace ExpenseSharing
{
    internal class Program
    {
        static List<string> SettleExpense(Dictionary<string, double> expenses)
        {
            List<string> settlement = new List<string>();

            Queue<KeyValuePair<string, double>> creditors = new Queue<KeyValuePair<string, double>>();
            Queue<KeyValuePair<string, double>> debitors = new Queue<KeyValuePair<string, double>>();

            var totalExpenses = expenses.Values.Sum();
            var persons = expenses.Count;
            var share = totalExpenses / persons;

            foreach (var person in expenses)
            {
                if (person.Value > share)
                {
                    creditors.Enqueue(
                        new KeyValuePair<string, double>(person.Key, Math.Abs(person.Value - share))
                        );
                }
                else if (person.Value < share)
                {
                    debitors.Enqueue(
                        new KeyValuePair<string, double>(person.Key, Math.Abs(person.Value - share))
                        );

                }

            }
            while (debitors.Count > 0 && creditors.Count > 0)
            {
                var payer = debitors.Dequeue();
                var receiver = creditors.Dequeue();
                var paymentAmount = Math.Min(payer.Value, receiver.Value);
                settlement.Add($"{payer.Key},{receiver.Key},{paymentAmount}");

                if (payer.Value > paymentAmount)
                {
                    debitors.Enqueue(new KeyValuePair<string, double>(payer.Key, Math.Abs(paymentAmount - payer.Value)));
                }
                if (receiver.Value > paymentAmount)
                {
                    debitors.Enqueue(new KeyValuePair<string, double>(receiver.Key, Math.Abs(paymentAmount - receiver.Value)));
                }
            }
            return settlement;
        }
        static void Main(string[] args)
        {
            Dictionary<string, double> dict = new Dictionary<string, double>()
            {
                {"Shreya",900.00},
                {"Sweta",0 },
                {"Sankar",1299 }
            };
            List<string> result = SettleExpense(dict);

            foreach(var i in result)
            {
                Console.WriteLine(i);
            }
        }
    }
}
