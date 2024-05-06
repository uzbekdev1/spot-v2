using System.ComponentModel.DataAnnotations;

namespace TraceApi.Models
{
    public class AuditLogModel
    {

        [Required]
        public string Title { get; set; }

        public string Note { get; set; }

        [Required]
        public string Category { get; set; }

        public int? UserId {  get; set; } 

    }

}
