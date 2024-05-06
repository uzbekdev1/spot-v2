using MessageBroker.Dtos;
using MessageBroker.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog; 
using System.Text;

namespace MessageBroker.Jobs
{
    public class BidWorker : BackgroundService
    {

        private readonly ILogger<BidWorker> _logger;

        private readonly IConfiguration _configuration;

        public BidWorker(ILogger<BidWorker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var serverHost = _configuration["ServerHost"];
            var apiUrl= _configuration["ApiUrl"];

            _logger.LogInformation("Waiting for bids.");

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "ClLbxh7qmq7h_s"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "bids", type: ExchangeType.Fanout);

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: "bids", routingKey: string.Empty);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"Received payload: {message}");

                var payload = JsonConvert.DeserializeObject<OrderPayload>(message);

                if (payload == null)
                {
                    return;
                }

                var service = new SpotService(apiUrl);
                
                service.SetToken(payload.token);

                var jobDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                service.CreateOrder(payload.traderId, payload.contractId, payload.kolvo, payload.inp, payload.price, payload.ip, payload.clientDate, payload.serverDate, jobDate, payload.newId, serverHost, payload.clientVersion, payload.dbDate);

                Log.Information($"Received order: {JsonConvert.SerializeObject(payload, Formatting.None)}");
                
                Log.Information($"Job time: {jobDate}");

            };
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }

        }

    }
}
