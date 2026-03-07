namespace LineMethod
{
    internal class Program
    {
        public static void Display(int num = 40, char ch = '-')
        {
            for (int i = 0; i < num; i++)
            {
                Console.Write(ch);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Display():");
            Display();
            Console.WriteLine();
            Console.WriteLine("Display(ch: '+'):");
            Display(ch: '+');
            Console.WriteLine();
            Console.WriteLine("Display(60, '$'):");
            Display(60, '$');

        }
    }
}
