using Vagabond.Web.Services;

namespace Vagabond.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<IDestinationService, DestinationService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7001/");
            });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Travel}/{action=Index}/{id?}");

            app.Run();
        }
    }
}