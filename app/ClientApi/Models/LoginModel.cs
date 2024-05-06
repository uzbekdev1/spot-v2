using System.ComponentModel.DataAnnotations;

namespace ClientApi.Models
{

    public class LoginModel
    {

        public string login { get; set; }

        public string password { get; set; }

        public string uid { get; set; }

        public string version {  get; set; }    

    }

    public class LoginEmbed
    {
    
        [Required]
        public string raw { get; set; }

    }

}
