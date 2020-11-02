//using MaReB.Data;
using MaReB.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using System;
//using Z.EntityFramework.Extensions;

namespace MaReB
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var context = services.GetRequiredService<ApplicationDbContext>();
            //    EntityFrameworkManager.ContextFactory = _context => { return context; };

            //    try
            //    {
            //        CountriesInitializer.Initialize(context);
            //        LocationsInitializer.Initialize(context);
            //        var env = services.GetRequiredService<IHostingEnvironment>();
            //        ArrivalsInitializer.Initialize(context, env.ContentRootPath);
            //        ExportsInitializer.Initialize(context);
            //        StationsInitializer.Initialize(context);
            //PuertosInitializer.Initialize(context);
            //ProcedenciasInitializer.Initialize(context);
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred while seeding the database.");
            //    }
            //}
            //host.Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                }).ConfigureKestrel(options =>
                {
                    options.Limits.MaxConcurrentConnections = 100;
                    options.Limits.MaxConcurrentUpgradedConnections = 100;
                    //options.Limits.MaxRequestBodySize = 20_000_000;
                    //options.Limits.MinRequestBodyDataRate =
                    //    new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                    //options.Limits.MinResponseDataRate =
                    //    new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                })
                .UseUrls("http://localhost:5007")
                .UseStartup<Startup>());
    }
}
