using Microsoft.EntityFrameworkCore;
using Vagabond.Service.Data;
using Vagabond.Service.Middleware;
using Vagabond.Service.Repositories;

namespace Vagabond.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 🔥 Add DbContext (EF Core)
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 🔥 Add Repository
            builder.Services.AddScoped<IDestinationRepository, DestinationRepository>();

            var app = builder.Build();

            // 🔥 Global Exception Middleware (IMPORTANT)
            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}