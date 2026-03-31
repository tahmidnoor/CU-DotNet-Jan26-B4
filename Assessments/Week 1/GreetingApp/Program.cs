using GreetingLibrary;

namespace GreetingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Your Name: ");
            string userName = Console.ReadLine();
            Console.WriteLine(Greet.Hello(userName));
        }
    }
}
