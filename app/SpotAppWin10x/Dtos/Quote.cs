namespace SpotApp.Dtos
{
    internal class Quote
    {
        
        public int traderId { get; set; }
        
        public int contractId { get; set; }
        
        public decimal cena { get; set; }
        
        public int amountBuy { get; set; }
        
        public int amountSell { get; set; }
  
        public int? _amountSell
        {
            get
            {
                if (amountSell == 0)
                    return null;
                return amountSell;
            }
        }
        
        public int countOrder { get; set; }
        
        public int brokerId { get; set; }
        
        public int? _brokerId
        {
            get
            {
                if (brokerId == 0)
                    return null;
                return brokerId;
            }
        }
        
        public int countPrice { get; set; }

    }
}
