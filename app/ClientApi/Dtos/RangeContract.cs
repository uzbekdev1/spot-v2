namespace ClientApi.Dtos
{
    public class RangeContract
    {
        
        public DateTime priceDate { get; set; }
        
        public decimal startPrice { get; set; }
        
        public decimal minPrice { get; set; }
        
        public decimal avgPrice { get; set; }
        
        public decimal maxPrice { get; set; }
        
        public decimal pricePercent { get; set; }

    }
}
