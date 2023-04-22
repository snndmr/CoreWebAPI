using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

namespace CompanyEmployees
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            // To observe middleware logic.
            /*

            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Logic before executing the next delegate in the Use method");
                await next.Invoke();
                Console.WriteLine($"Logic after executing the next delegate in the Use method");
            });

            app.Map("/map", applicationBuilder =>
            {
                applicationBuilder.Use(async (context, next) =>
                {
                    Console.WriteLine("Map branch logic in the Use method before the next delegate");
                    await next.Invoke();
                    Console.WriteLine("Map branch logic in the Use method after the next delegate");
                });
                applicationBuilder.Run(async context =>
                {
                    Console.WriteLine($"Map branch response to the client in the Run method");
                    await context.Response.WriteAsync("Hello from the map branch.");
                });
            });

            app.MapWhen(context => context.Request.Query.ContainsKey("query"), applicationBuilder =>
            {
                applicationBuilder.Run(async context =>
                {
                    await context.Response.WriteAsync("Hello from the MapWhen branch.");
                });
            });

            app.Run(async context =>
            {
                Console.WriteLine($"Writing the response to the client in the Run method");
                await context.Response.WriteAsync("Hello from the middleware component.");
            });

            */

            app.MapControllers();

            app.Run();
        }
    }
}