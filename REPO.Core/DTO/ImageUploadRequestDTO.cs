using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace REPO.Core.DTO
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }


    }

}
