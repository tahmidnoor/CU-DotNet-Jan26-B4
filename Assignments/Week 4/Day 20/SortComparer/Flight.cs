namespace SortComparer
{
    class DurationComparer : IComparer<Flight>
    {
        public int Compare(Flight? x, Flight? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Duration.CompareTo(y.Duration);
        }
    }
    class DepartureComparer : IComparer<Flight>
    {

        public int Compare(Flight? x, Flight? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.DepartureTime.CompareTo(y.DepartureTime);
        }
    }
    internal class Flight : IComparable<Flight>
    {
        public string FlightNumber { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }

        public DateTime DepartureTime { get; set; }

        public Flight() { }
        public Flight(string FNumber, decimal Pr, TimeSpan Dur, DateTime DT)
        {
            FlightNumber = FNumber;
            Price = Pr;
            Duration = Dur;
            DepartureTime = DT;
        }
        public int CompareTo(Flight? other)
        {
            if (other == null)
                return 1;
            return this.Price.CompareTo(other.Price);
        }
        public override string ToString()
        {
            return $"{FlightNumber} | ₹{Price} | {Duration} | {DepartureTime}";
        }

        static void Main(string[] args)
        {
            List<Flight> flights = new List<Flight> {
                new Flight
                {
                    FlightNumber = "SH101",
                    Price = 4500,
                    Duration = TimeSpan.FromHours(2.5),
                    DepartureTime = new DateTime(2026, 2, 1, 6, 30, 0)
                },
                new Flight
                {
                    FlightNumber = "SH202",
                    Price = 3200,
                    Duration = TimeSpan.FromHours(3),
                    DepartureTime = new DateTime(2026, 2, 1, 9, 0, 0)
                },
                new Flight
                {
                    FlightNumber = "SH303",
                    Price = 5000,
                    Duration = TimeSpan.FromHours(2),
                    DepartureTime = new DateTime(2026, 2, 1, 5, 45, 0)
                }

            };


            Console.OutputEncoding = System.Text.Encoding.UTF8;

            flights.Sort();
            Console.WriteLine("Economy View (Cheapest First)");
            flights.ForEach(Console.WriteLine);

            flights.Sort(new DurationComparer());
            Console.WriteLine("\nBusiness Runner View (Shortest First)");
            flights.ForEach(Console.WriteLine);

            flights.Sort(new DepartureComparer());
            Console.WriteLine("\nEarly Bird View (Earliest First)");
            flights.ForEach(Console.WriteLine);
        }

    }
}
