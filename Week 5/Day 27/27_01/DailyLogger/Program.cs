namespace DailyLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fl = "journal.txt";
            string dir = "../../../";
            string path = dir + fl;
            Console.Write("Enter your Daily Reflection: ");
            string refl = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine("Date: " + DateTime.Now);
                writer.WriteLine(refl);
                writer.WriteLine("---------------------");
            }
            Console.WriteLine("Reflection Saved!");
        }
    }
}
