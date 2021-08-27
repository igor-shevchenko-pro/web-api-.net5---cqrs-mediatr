using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Notes.Persistence;

namespace Notes.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                                                  .WriteTo.File("NotesWebAppLog-.txt", rollingInterval: RollingInterval.Day)
                                                  .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            using(var scope = host.Services.CreateScope())
            {
                var servicePropvider = scope.ServiceProvider;
                try
                {
                    var context = servicePropvider.GetRequiredService<NotesDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception exception)
                {
                    //var logger = servicePropvider.GetRequiredService<ILogger<Program>>();
                    //logger.LogError(exception, "An error occurred while while app initializing");

                    Log.Fatal(exception, "An error occurred while app initialization");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
