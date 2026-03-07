namespace HeightClass
{
    class Height
    {
        public int Feet {  get; set; }
        public double Inches { get; set; }

        public Height()
        {
            Feet = 0;
            Inches = 0;
        }
        public Height(int feet, double inches)
        {
            Feet = feet;
            Inches = inches;
        }
        public Height AddHeights(Height h2)
        {
            int tFeet = this.Feet + h2.Feet;
            double tInches = this.Inches + h2.Inches;

            if (tInches >= 12)
            {
                tFeet += (int)(tInches / 12);
                tInches = tInches % 12;
            }

            return new Height(tFeet, tInches);
        }

        public override string ToString()
        {
            return $"Height - {Feet} feet {Inches:F1} inches";
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Height p1 = new Height(5, 6.5);
            Height p2 = new Height(5, 7.5);

            Height total = p1.AddHeights(p2);

            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine(total);
        }
    }
}
