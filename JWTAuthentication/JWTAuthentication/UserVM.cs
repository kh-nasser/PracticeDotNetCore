using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication
{
    public class UserVM
    {
        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^.{5,50}$", ErrorMessage = "String length must be greater than or equal 5 characters.")]
        public string Password { get; set; }
    }
}
