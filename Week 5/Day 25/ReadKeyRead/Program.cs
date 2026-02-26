namespace ReadKeyRead
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pin = "";
            Console.Write("Enter 4-digit PIN: ");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);


                if (char.IsDigit(key.KeyChar) && pin.Length < 4)
                {
                    pin += key.KeyChar;
                    Console.Write("*");
                }

                else if (key.Key == ConsoleKey.Backspace && pin.Length > 0)
                {
                    pin = pin.Substring(0, pin.Length - 1);

                    Console.Write("\b \b");
                }

                if (pin.Length == 4)
                    break;
            }
            Console.WriteLine("\nPIN Captured Successfully!");
            Console.Write("\nEnter System Message: ");
            string message = Console.ReadLine();

            Console.Write("\nSystem Message Received: ");
            Console.WriteLine(message);

        }
    }
}
