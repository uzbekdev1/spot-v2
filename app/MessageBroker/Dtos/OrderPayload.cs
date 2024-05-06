namespace MessageBroker.Dtos
{
    public class OrderPayload
    {

        public int traderId { get; set; }

        public int contractId { get; set; }

        public int kolvo { get; set; }

        public int inp { get; set; }

        public decimal price { get; set; }

        public string ip { get; set; }

        public string clientDate { get; set; }
        
        public string serverDate { get; set; }

        public string newId { get; set; }

        public string token { get; set; }

        public string clientVersion { get; set; }

        public string dbDate { get; set; }
    }
}
