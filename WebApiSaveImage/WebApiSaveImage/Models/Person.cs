using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSaveImage.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = null!;
        [Column(TypeName = "nvarchar(100)")]
        public string Family { get; set; } = null!;
        [Column(TypeName = "nvarchar(100)")]
        public string Mobile { get; set; } = null!;
        [Column(TypeName = "nvarchar(100)")]
        public string? ImageName { get; set; }
    }
}
