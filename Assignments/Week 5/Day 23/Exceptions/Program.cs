namespace Exceptions
{
    class InvalidStudentAgeException : Exception
    {
        public InvalidStudentAgeException(string message) : base(message) { }
    }
    class InvalidStudentNameException : Exception
    {
        public InvalidStudentNameException(string message) : base(message) { }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter First number: ");
                int a = int.Parse(Console.ReadLine());

                Console.Write("Enter Second number: ");
                int b = int.Parse(Console.ReadLine());

                Console.WriteLine($"Result: {a / b}");

            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: Cannot divide by zero.");

            }
            finally
            {
                Console.WriteLine("Operation Completed!");
            }
            Console.WriteLine();
            try
            {
                Console.Write("Enter an Integer: ");
                int num = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid number format.");
            }
            finally
            {
                Console.WriteLine("Operation Completed!");
            }
            Console.WriteLine();
            try
            {
                int[] arr = { 10, 20, 30 };
                Console.Write("Enter Index: ");
                int index = int.Parse(Console.ReadLine());
                Console.WriteLine(arr[index]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Error: Index out of range.");
            }
            finally
            {
                Console.WriteLine("Operation Completed!");
            }
            Console.WriteLine();

            try
            {
                Console.Write("Enter Student Name: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new InvalidStudentNameException("Student name cannot be empty.");
                }
            }
            catch (InvalidStudentNameException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
            int age = 0;
            bool validAge = false;
            while (!validAge)
            {
                try
                {
                    Console.Write("Enter Student Age: ");
                    age = int.Parse(Console.ReadLine());
                    if (age < 18 || age > 60)
                    {
                        throw new InvalidStudentAgeException("Age must be between 18 and 60.");
                    }
                    validAge = true;

                }
                catch (InvalidStudentAgeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Age must be a number.");
                }
            }
            try
            {
                try
                {
                    throw new InvalidStudentAgeException("Invalid age entered.");
                }
                catch (InvalidStudentAgeException ex)
                {
                    throw new Exception("Student validation failed.", ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                Console.WriteLine("InnerException: " + ex.InnerException.Message);
            }

        }
    }
}
