namespace ClientApi.Dtos
{
    public class Quote
    {

        public int traderId { get; set; }

        public int contractId { get; set; }

        public decimal cena { get; set; }

        public int amountBuy { get; set; }

        public int amountSell { get; set; }

        public int countOrder { get; set; }

        public int brokerId { get; set; }

        public int countPrice { get; set; }

    }
}
