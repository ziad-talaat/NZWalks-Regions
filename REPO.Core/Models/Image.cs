using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace REPO.Core.Models
{
    public class Image
    {
        public  Guid Id { get; set; }  =Guid.NewGuid();

        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }

        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }

        public Guid? WalkId { get; set; }
        public Guid? RegionId { get; set; }

        [ForeignKey(nameof(Image.WalkId))]
        public  Walk Walk { get; set; }

        [ForeignKey(nameof(Image.RegionId))]
        public  Region Region { get; set; }
    }
}
