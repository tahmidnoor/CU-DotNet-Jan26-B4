namespace SmartAccessControl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Entry Access Info.: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("INVALID ACCESS LOG!");
                return;
            }

            string[] inputs = input.Split('|');

            if (inputs.Length != 5)
            {
                Console.WriteLine("INVALID ACCESS LOG!");
                return;
            }

            string gateCode = inputs[0];
            if (gateCode.Length != 2 ||
                !char.IsLetter(gateCode[0]) ||
                !char.IsDigit(gateCode[1]))
            {
                Console.WriteLine("INVALID ACCESS LOG!");
                return;
            }

            if (inputs[1].Length != 1)
            {
                Console.WriteLine("INVALID ACCESS LOG!");
                return;
            }

            char userInitial = inputs[1][0];
            if (!char.IsUpper(userInitial))
            {
                Console.WriteLine("INVALID ACCESS LOG!");
                return;
            }

            if (!byte.TryParse(inputs[2], out byte accessLevel) ||
                accessLevel < 1 || accessLevel > 7)
            {
                Console.WriteLine("INVALID ACCESS LOG!");
                return;
            }

            if (!bool.TryParse(inputs[3], out bool isActive))
            {
                Console.WriteLine("INVALID ACCESS LOG!");
                return;
            }

            if (!byte.TryParse(inputs[4], out byte attempts) ||
                attempts > 200)
            {
                Console.WriteLine("INVALID ACCESS LOG!");
                return;
            }

            string status;

            if (!isActive)
            {
                status = "ACCESS DENIED – INACTIVE USER";
            }
            else if (attempts > 200)
            {
                status = "ACCESS DENIED – TOO MANY ATTEMPTS";
            }
            else if (accessLevel >= 5)
            {
                status = "ACCESS GRANTED – HIGH SECURITY";
            }
            else
            {
                status = "ACCESS GRANTED – STANDARD";
            }

            Console.WriteLine($"{"Gate",-10}: {gateCode}");
            Console.WriteLine($"{"User",-10}: {userInitial}");
            Console.WriteLine($"{"Level",-10}: {accessLevel}");
            Console.WriteLine($"{"Attempts",-10}: {attempts}");
            Console.WriteLine($"{"Status",-10}: {status}");
        }
    }
}
