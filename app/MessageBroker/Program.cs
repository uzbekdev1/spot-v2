using MessageBroker.Jobs;
using Serilog;

namespace MessageBroker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Message Broker";

            Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Debug()
          .WriteTo.File("Logs/broker.log", rollingInterval: RollingInterval.Day)
          .CreateLogger();

            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.Configure<HostOptions>(hostOptions =>
            {
                hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
            });
            builder.Services.AddHostedService<BidWorker>();

            var host = builder.Build();
            host.Run();

            Log.CloseAndFlush();
        }
    }
}