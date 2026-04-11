
using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace LibraryManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() 
    .WriteTo.Console()        
    .WriteTo.File("logs/myapp-.txt", rollingInterval: RollingInterval.Day) 
    .CreateLogger();
            builder.Host.UseSerilog();

            builder.Services.AddDbContext<MyAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            var app = builder.Build();
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Unhandled Exception");

                    context.Response.StatusCode = 500;

                    // 👇 TEMP: show real error
                    await context.Response.WriteAsync(ex.Message);
                }
            });
            //app.Use(async (context, next) =>
            //{
            //    try
            //    {
            //        await next();
            //    }
            //    catch (Exception ex)
            //    {
            //        Log.Error(ex, "Unhandled Exception");

            //        context.Response.StatusCode = 500;
            //        await context.Response.WriteAsync("Internal Server Error");
            //    }
            //});


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();
            app.MapControllers();

            app.Run();
        }
    }
}
