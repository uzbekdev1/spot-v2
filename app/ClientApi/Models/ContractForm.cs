using System.ComponentModel.DataAnnotations;

namespace ClientApi.Models
{
    public class ConractEmbed
    {

        [Required]
        public string raw { get; set; }

    }

    public class ContractForm
    {

        public int contractId { get; set; }

        public string uid { get; set; }

    }

}
