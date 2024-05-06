using System.ComponentModel.DataAnnotations;

namespace TraceApi.Models
{
    public class AppErrorModel
    {

        [Required]
        public string Title { get; set; }

        public string Note { get; set; }

        public int? UserId { get; set; }

    }

}
