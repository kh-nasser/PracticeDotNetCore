using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication
{
    public class UserModel
    {
        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^(?:.*[a-z]){7,}$", ErrorMessage = "String length must be greater than or equal 7 characters.")]
        public string Password { get; set; }
    }
}
