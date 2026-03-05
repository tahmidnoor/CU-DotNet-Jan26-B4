namespace StreamCollec
{
    class CreatorStats
    {
        public string CreatorName { get; set; }
        public double[] WeeklyLikes { get; set; }

        public CreatorStats(string name, double[] likes)
        {
            CreatorName = name;
            WeeklyLikes = likes;
        }
    }

    internal class Program
    {
        public static List<CreatorStats> EngagementBoard = new List<CreatorStats>();

        public static void RegisterCreator(CreatorStats record)
        {
            EngagementBoard.Add(record);
        }

        public static Dictionary<string, int> GetTopPostCounts(List<CreatorStats> creatorStats, double likeThreshold)
        {
            Dictionary<string, int> topPost = new Dictionary<string, int>();
            foreach (var creator in creatorStats)
            {
                int cnt = 0;
                foreach (var like in creator.WeeklyLikes)
                {
                    if (like >= likeThreshold)
                    {
                        cnt++;
                    }
                }
                if (cnt > 0)
                {
                    topPost[creator.CreatorName] = cnt;
                }
            }
            return topPost;
        }

        public static double CalculateAverageLikes()
        {
            List<double> avgs = new List<double>();
            foreach(var creator in EngagementBoard)
            {
                avgs.Add(creator.WeeklyLikes.Average());
            }
            double avgLike = avgs.Average();
            return avgLike;
        }

        static void Main(string[] args)
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("1. Register Creator");
                Console.WriteLine("2. Show Top Posts");
                Console.WriteLine("3. Calculate Average Likes");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                int ch = int.Parse(Console.ReadLine());
                Console.WriteLine();
                switch (ch)
                {
                    case 1:
                        Console.Write("Enter Creator Name: ");
                        string name = Console.ReadLine();
                        double[] likes = new double[4];
                        Console.WriteLine("Enter weekly likes (Week 1 to 4):");
                        for(int i = 0; i < 4; i++)
                        {
                            likes[i] = (double)int.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("Creator registered successfully!");
                        RegisterCreator(new CreatorStats(name, likes));
                        Console.WriteLine();
                        break;

                    case 2:
                        Console.Write("Enter Like Threshold: ");
                        double maxLikes = (double)int.Parse(Console.ReadLine());
                        Dictionary<string, int> res = GetTopPostCounts(EngagementBoard, maxLikes);
                        if(res.Count == 0)
                        {
                            Console.WriteLine("No top-performing posts this week!");
                        }
                        else
                        {
                            foreach (var i in res)
                            {
                                Console.WriteLine($"{i.Key} - {i.Value}");
                            }
                        }
                        Console.WriteLine();
                        break;

                    case 3:
                        double avg = CalculateAverageLikes();
                        Console.WriteLine($"Overall average weekly likes: {avg}");
                        Console.WriteLine();
                        break;

                    case 4:
                        Console.WriteLine("Logging off - Keep Creating with StreamBuzz!");
                        flag = false;
                        Console.WriteLine();
                        break;

                    default:
                        Console.WriteLine("Enter valid choice!");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
