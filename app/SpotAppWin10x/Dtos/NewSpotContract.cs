namespace SpotApp.Dtos
{
    internal class NewSpotContract
    {
        public int id { get; set; }

        public string name { get; set; }

        public decimal? demand { get; set; }

        public decimal? offer { get; set; }

        public string newSpotContractNumber { get; set; }
    }
}
