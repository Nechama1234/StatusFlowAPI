
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StatusFlowAPI.Models;

namespace StatusFlowAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // public void ConfigureServices(IServiceCollection services)
            //{
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer("Server=DESKTOP-7QELS7G;Database=StatusFlow;Integrated Security=True;TrustServerCertificate=True;"));

                // other service configurations
           // }
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
