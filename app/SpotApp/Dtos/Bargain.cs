using System;

namespace SpotApp.Dtos
{
    internal class Bargain
    {

        public int BargainId { get; set; }
        
        public DateTime Datepost { get; set; }
        
        public int ContractId { get; set; }
        
        public string TradeType { get; set; }
        
        public int Kolvo { get; set; }
        
        public decimal Cena { get; set; }
        
        public decimal Cost { get; set; }
        
        public string FullName { get; set; }
        
        public decimal Prepay { get; set; }

    }
}
