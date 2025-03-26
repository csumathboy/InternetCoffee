

using Microsoft.OpenApi.Models;

namespace InternetCoffee.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Coffee API",
                    Version = "v1",
                    Description = "A fun API that refuses to brew coffee (HTTP 418)."
                });
            });

            //enject application and infrastructure services
            builder.AddApplicationServices();

            //enject infrastructure services
            builder.AddInfrastructureServices();

            var app = builder.Build();

            // Enable Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapControllerRoute(
                name: "brew-coffee",
                pattern: "{controller=BrewCoffee}/{action=BrewCoffee}");
 
            app.Run();
        }
    }
}
