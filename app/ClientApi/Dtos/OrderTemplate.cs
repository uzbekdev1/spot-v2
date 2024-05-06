namespace ClientApi.Dtos
{
    public class OrderTemplate
    {
        public int id { get; set; }
        public int contractId { get; set; }
        public string contractName { get; set; }
        public int inp { get; set; }
        public string clientName { get; set; }
        public decimal price { get; set; }
        public int kolvo { get; set; }
        public int maxPriceCount { get; set; }

    }
}
