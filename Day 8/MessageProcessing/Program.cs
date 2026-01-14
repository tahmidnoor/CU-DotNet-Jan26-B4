namespace MessageProcessing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Login Message: ");
            string input = Console.ReadLine();

            string[] inputs = input.Split('|');
            
            string uName = inputs[0];
            string msg = inputs[1];

            string cMsg = msg.Trim().ToLower();

            string status = string.Empty;

            Console.WriteLine($"User\t:{uName}");
            Console.WriteLine($"Message\t:{cMsg}");

            if (string.IsNullOrWhiteSpace(input))
            {
                status = "LOGIN FAILED!";
            }
            else if (!cMsg.Contains("successful"))
            {
                status = "LOGIN FAILED!";
            }
            else
            {
                if (cMsg.Equals("login successful"))
                {
                    status = "LOGIN SUCCESS!";
                }
                else
                {
                    status = "LOGIN SUCCESS! (CUSTOM MESSAGE)";
                }
            }
            Console.WriteLine($"Status\t:{status}");
        }
    }
}

