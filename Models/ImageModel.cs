using System.ComponentModel.DataAnnotations;

namespace ConsumeOMS.Models
{
    public class ImageModel
    {
        [Required]
        public string? ServiceName { get; set; }
        [Required]
        public string? serviceDescription { get; set; }
        [Required]

        public IFormFile image { get; set; }
    }
}
