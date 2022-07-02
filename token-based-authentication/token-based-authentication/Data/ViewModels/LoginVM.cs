using System.ComponentModel.DataAnnotations;

namespace token_based_authentication.Data.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string EmailAddrress{ get; set; }
        [Required]
        public string Password{ get; set; }
    }
}
