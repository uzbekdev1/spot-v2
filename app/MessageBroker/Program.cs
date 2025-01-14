using MessageBroker.Jobs;
using Serilog;
using System.Diagnostics;

namespace MessageBroker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Message Broker";

            var host = Host.CreateDefaultBuilder(args)
                .UseSerilog((hst, cnf) =>
                {
                    cnf.ReadFrom.Configuration(hst.Configuration);
                    cnf.Enrich.FromLogContext();
                    cnf.Enrich.WithProperty("ApplicationName", hst.HostingEnvironment.ApplicationName);
                    cnf.MinimumLevel.Debug();
                    if (Debugger.IsAttached)
                        cnf.WriteTo.Console();
                    cnf.WriteTo.File("Logs/broker.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true);
                })
                .ConfigureServices(services =>
                {
                    services.Configure<HostOptions>(hostOptions =>
                    {
                        hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
                    });

                    services.AddHostedService<BidWorker>();
                })
                .Build();

            host.Run();
        }
    }
}