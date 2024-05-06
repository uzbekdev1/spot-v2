using Newtonsoft.Json;

namespace SpotApp.Models
{
    internal class OrderForm
    {

        [JsonProperty(PropertyName = "a")]
        public int contractId { get; set; }

        [JsonProperty(PropertyName = "b")]
        public string uid { get; set; }

        [JsonProperty(PropertyName = "c")]
        public int inp { get; set; }

        [JsonProperty(PropertyName = "d")]
        public int kolvo { get; set; }

        [JsonProperty(PropertyName = "e")]
        public decimal price { get; set; }

        [JsonProperty(PropertyName = "f")]
        public string clientDate { get; set; }

        [JsonProperty(PropertyName = "g")]
        public string clientVersion { get; set; }

        [JsonIgnore]
        [JsonProperty(PropertyName = "h")]
        public string contractName { get; set; }

        [JsonIgnore]
        [JsonProperty(PropertyName = "i")]
        public string inpName { get; set; }

        [JsonIgnore]
        [JsonProperty(PropertyName = "j")]
        public int windowOrder { get; set; }

        [JsonIgnore]
        [JsonProperty(PropertyName = "k")]
        public decimal contractStartPrice { get; set; }

        [JsonProperty(PropertyName = "l")]
        public string dbDate { get; set; }
    }
}
