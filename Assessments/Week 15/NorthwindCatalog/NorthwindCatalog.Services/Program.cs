using Microsoft.EntityFrameworkCore;
using NorthwindCatalog.Services.Data;
using NorthwindCatalog.Services.Repositories;
using NorthwindCatalog.Services.Mapping;

namespace NorthwindCatalog.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<NorthwindContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

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