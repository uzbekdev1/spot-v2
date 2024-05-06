namespace ClientApi.Dtos
{
    public class ContactItem
    {

        public int contractId { get; set; }

        public string name { get; set; }

        public string unit { get; set; }

        public int lot { get; set; }

        public decimal price { get; set; }

        public string starttime { get; set; }

        public string endtime { get; set; }
    }
}
