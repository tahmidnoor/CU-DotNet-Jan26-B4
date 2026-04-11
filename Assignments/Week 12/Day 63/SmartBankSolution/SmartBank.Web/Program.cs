using SmartBank.Web.Services;

namespace SmartBank.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            builder.Services.AddHttpClient<AuthApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7000/");
            });

            builder.Services.AddHttpClient<AccountApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7000/");
            });

            builder.Services.AddHttpClient<TransactionApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7000/");
            });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}