using ClientApi.Helpers;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ClientApi.Services
{
    public class AmqpService
    {
        private IModel _channel;

        private readonly ILogger<AmqpService> _logger;

        public AmqpService(ILogger<AmqpService> logger)
        {
            _logger = logger;
        }

        public void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    Port = 5672,
                    UserName = "guest",
                    Password = "ClLbxh7qmq7h_s"
                };
                var connection = factory.CreateConnection();

                _channel = connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "bids", type: ExchangeType.Fanout);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { }));
            }
        }

        public void PushMessage(int traderId, int contractId, int kolvo, int inp, decimal price, string ip, string clientDate, string serverDate, string newId, string clientVersion, string token, string dbDate, int orderType)
        {
            var methodStage = "";

            try
            {
                var message = JsonConvert.SerializeObject(new
                {
                    traderId,
                    contractId,
                    kolvo,
                    inp,
                    price,
                    ip,
                    clientDate,
                    serverDate,
                    newId,
                    clientVersion,
                    token,
                    dbDate,
                    orderType
                });
                var body = Encoding.UTF8.GetBytes(message);

                methodStage = "channel BasicPublish";

                _channel.BasicPublish(exchange: "bids", routingKey: string.Empty, basicProperties: null, body: body);
            }
            catch (Exception exp)
            {
                throw new Exception($"Exception pushMessage: {exp.Message}; methodStage:{methodStage}");
            }
        }

    }
}
