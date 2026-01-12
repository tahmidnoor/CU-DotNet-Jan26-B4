namespace GreetingLibrary
{
    public class Greet
    {
        public static string Hello(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Hello, Guest!";
            }
            else
            {
                return $"Hello, {name}!";
            }
        }
    }
}
