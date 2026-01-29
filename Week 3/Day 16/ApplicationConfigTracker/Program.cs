namespace ApplicationConfigTracker
{
    class AppConfig
    {
        public static string ApplicationName {  get; set; }
        public static string Environment {  get; set; }
        public static int AccessCount { get; set; }
        public static bool IsInitialized { get; set; }

        static AppConfig()
        {
            ApplicationName = "MyApp";
            Environment = "Development";
            AccessCount = 0;
            IsInitialized = false;

            Console.WriteLine("Static Constructor Called!");
        }

        public static void Initialize(string appName, string environment)
        {
            ApplicationName = appName;
            Environment = environment;
            IsInitialized = true;
            AccessCount++;
        }

        public static string GetConfigurationSummary()
        {
            AccessCount++;
            return $"App Name\t: {ApplicationName}\nEnviroment\t: {Environment}\nAccess Count\t: {AccessCount}\nInitialized\t: {IsInitialized}";
        }

        public static void ResetConfiguration()
        {
            ApplicationName = "MyApp";
            Environment = "Development";
            AccessCount++;
            IsInitialized = false;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string app = AppConfig.ApplicationName;

            AppConfig.Initialize("AppConfigTracker", "Build");

            string sum = AppConfig.GetConfigurationSummary();
            Console.WriteLine();
            Console.WriteLine("Configuration");
            Console.WriteLine("-------------");
            Console.WriteLine(sum);

            AppConfig.ResetConfiguration();

            string rsum = AppConfig.GetConfigurationSummary();
            Console.WriteLine();
            Console.WriteLine("Reset Configuration");
            Console.WriteLine("-------------------");
            Console.WriteLine(rsum);
        }
    }
}
