using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Users
{
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public string UserName { get; set; }
        [MinLength(6)]
        [MaxLength(50)]
        public int Password { get; set; }
        [MaxLength(110)]
        public int Avatar { get; set; }
    }
}
