using System.Text.Json.Serialization;

namespace ClientApi.Dtos
{
    public class MainContact
    {

        [JsonPropertyName("id")]
        public int par1 { get; set; }

        [JsonPropertyName("name")]
        public string par2 { get; set; }

        [JsonPropertyName("demand")]
        public decimal? par3 { get; set; }

        [JsonPropertyName("offer")]
        public decimal? par4 { get; set; }

        [JsonIgnore]
        public int par5 { get; set; }

        public bool exhibition => par5 == 1;

        public string newSpotContractNumber { get; set; }

    }
}
