using AzureCustomVision.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace AzureCustomVision.API.Requests
{
    public class FileRequest
    {
        [Required]
        [MaxFileSize(1 * 1024 * 1024)]
        [AllowedExtensions([".jpg", ".jpeg", ".png"])]
        public IFormFile Image { get; set; }
    }
}
