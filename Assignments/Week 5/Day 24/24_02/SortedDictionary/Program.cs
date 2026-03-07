namespace SortedDictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<double, string> leaderboard = new SortedDictionary<double, string>();
            leaderboard.Add(55.42, "SwiftRacer");
            leaderboard.Add(52.10, "SpeedDemon");
            leaderboard.Add(58.91, "SteadyEddie");
            leaderboard.Add(51.05, "TurboTom");

            Console.WriteLine("LeaderBoard:");
            foreach (var entry in leaderboard)
            {
                Console.WriteLine($"{entry.Value} - {entry.Key}");
            }
            var fatest = leaderboard.First();
            Console.WriteLine($"\nGold Medal: {fatest.Value}-{fatest.Key}");

            leaderboard.Remove(58.91);
            leaderboard.Add(54.00, "SteadyEddie");

            Console.WriteLine("\nUpdated Leaderboard:");
            foreach (var entry in leaderboard)
            {
                Console.WriteLine($"{entry.Value} - {entry.Key} sec");

            }
        }
    }
}
