using LogiTrack.IdentityService.Services;

namespace LogiTrack.IdentityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ---------------- SERVICES ----------------

            // Register Token Service
            builder.Services.AddScoped<ITokenService, TokenService>();

            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ---------------- PIPELINE ----------------

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // No authentication needed here (this service only issues tokens)
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}