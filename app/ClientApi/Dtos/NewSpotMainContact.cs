namespace ClientApi.Dtos
{
    public class NewSpotMainContact
    {

        public int id { get; set; }

        public string number { get; set; }

        public string productName { get; set; }

        public int amount { get; set; }

        public string unit { get; set; }

        public decimal startPrice { get; set; }

        public int lot { get; set; }

        public string warehouse { get; set; }

        public string schedule { get; set; }

        public int contractId { get; set; }

        public string sellerName { get; set; }

    }
}
