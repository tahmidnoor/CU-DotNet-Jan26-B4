using Yarp.ReverseProxy;

namespace SmartBank.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ---------------- SERVICES ----------------

            // Add YARP Reverse Proxy
            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            var app = builder.Build();

            // ---------------- PIPELINE ----------------

            app.UseHttpsRedirection();

            // Optional: root test endpoint
            app.MapGet("/", () => "SmartBank API Gateway Running 🚀");

            // 🔥 Core: Map all proxy routes
            app.MapReverseProxy();

            app.Run();
        }
    }
}