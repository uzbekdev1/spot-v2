using System.Text.Json.Serialization;

namespace ClientApi.Dtos
{
    public class OrderItem
    {

        [JsonIgnore]
        public int orderId { get; set; }

        [JsonIgnore]
        public string orderDate { get; set; }

        [JsonIgnore]
        public int contractId { get; set; }

        [JsonIgnore]
        public int inp { get; set; }

        [JsonIgnore]
        public int traderid { get; set; }

        public decimal cena { get; set; }

        public int kolvo { get; set; }

        public string status { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public bool mine { get; set; }

    }
}
