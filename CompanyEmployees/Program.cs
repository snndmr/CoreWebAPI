using AspNetCoreRateLimit;
using CompanyEmployees.Extensions;
using CompanyEmployees.Presentation.ActionFilters;
using Contract;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;
using Service;
using Shared.DataTransferObjects;

namespace CompanyEmployees
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            // Add services to the container.

            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.AddScoped<ValidationFilterAttribute>();
            builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
            builder.Services.ConfigureResponseCaching();
            builder.Services.ConfigureHttpCacheHeaders();
            builder.Services.AddMemoryCache();
            builder.Services.ConfigureRateLimitingOptions();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddControllers(config =>
                {
                    config.RespectBrowserAcceptHeader = true;
                    config.ReturnHttpNotAcceptable = true;
                    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
                    config.CacheProfiles.Add("30SecondsDuration", new CacheProfile { Duration = 30 });
                })
                .AddXmlDataContractSerializerFormatters()
                .AddCustomCsvFormatter()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            var logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);

            if (!app.Environment.IsProduction())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseIpRateLimiting();
            app.UseCors("CorsPolicy");
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

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

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
            new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
                .Services.BuildServiceProvider()
                .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>().First();
    }
}