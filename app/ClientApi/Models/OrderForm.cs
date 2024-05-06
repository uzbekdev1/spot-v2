using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ClientApi.Models
{
    public class OrderEmbed
    {

        [Required]
        public string raw { get; set; }

    }

    public class OrderForm
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

        [JsonProperty(PropertyName = "l")]
        public string dbDate { get; set; }
    }

}
