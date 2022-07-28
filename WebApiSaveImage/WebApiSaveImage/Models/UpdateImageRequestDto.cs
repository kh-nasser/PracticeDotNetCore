using System.ComponentModel.DataAnnotations;

namespace WebApiSaveImage.Models
{
    public class UpdateImageRequestDto
    {
        [Required]
        public IFormFile FileAttach { get; set; } = null!;
    }
}
