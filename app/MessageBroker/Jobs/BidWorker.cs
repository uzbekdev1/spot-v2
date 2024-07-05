using MessageBroker.Dtos;
using MessageBroker.Enums;
using MessageBroker.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
            var apiUrl = _configuration["ApiUrl"];

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
                var uid = "";
                try
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var payload = JsonConvert.DeserializeObject<OrderPayload>(message);

                    if (payload == null)
                    {
                        _logger.LogError($"Received payload is null message:{message}");
                        return;
                    }

                    uid = payload.newId;

                    var service = new SpotService(apiUrl);

                    var jobDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    if (payload.orderType == (int)OrderTypes.CreateOrderV2 || payload.orderType == (int)OrderTypes.BulkOrder)
                    {
                        service.CreateOrder(payload.traderId, payload.contractId, payload.kolvo, payload.inp, payload.price, payload.ip, payload.clientDate, payload.serverDate, jobDate, payload.newId, serverHost, payload.clientVersion, payload.dbDate);
                    }
                    else if (payload.orderType == (int)OrderTypes.CreatePostOrderV2)
                    {
                        service.CreatePostOrder(payload.traderId, payload.contractId, payload.kolvo, payload.inp, payload.price, payload.ip, payload.clientDate, payload.serverDate, jobDate, payload.newId, serverHost, payload.clientVersion, payload.dbDate);
                    }
                    else
                    {
                        _logger.LogError($"Incorrect payload orderType:{payload.orderType}; Received order: {JsonConvert.SerializeObject(payload, Formatting.None)}");
                        return;
                    }

                    _logger.LogInformation($"Job time: {jobDate}; Received order: {JsonConvert.SerializeObject(payload, Formatting.None)}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"uid: {uid}; Error: {ex.Message}");
                }
            };
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
