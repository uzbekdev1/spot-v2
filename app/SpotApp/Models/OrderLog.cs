using Newtonsoft.Json;

namespace SpotApp.Models
{
    internal class OrderLog
    {
        [JsonProperty(PropertyName = "b")]
        public string uid { get; set; }

        [JsonProperty(PropertyName = "j")]
        public int windowOrder { get; set; }

        [JsonProperty(PropertyName = "g")]
        public string clientVersion { get; set; }
    }
}
