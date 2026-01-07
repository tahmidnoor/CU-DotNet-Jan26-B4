namespace ExamResult
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Tahmid!");
            int math = 91;
            int os = 92;
            int dsa = 92;
            int eng = 89;
            int cn = 95;
            double res = (math + os + dsa + eng + cn) / 5.00;
            Console.WriteLine($"Your Result: {res:F2}");

            if((int)res > 80)
            {
                Console.WriteLine("Your are Eligible for the Scholarship!");
            }
            else
            {
                Console.WriteLine("Your are not Eligible for the Scholarship!");
            }
        }
    }
}
