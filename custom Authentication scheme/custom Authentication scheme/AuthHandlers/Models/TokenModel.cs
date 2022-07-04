using System.ComponentModel.DataAnnotations;

namespace custom_Authentication_scheme.Authentication.Data
{
    public class TokenModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
