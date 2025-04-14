using System.ComponentModel.DataAnnotations;

namespace REPO.Core.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        [Required]
        public string Code { get; set; }
        [Required, MaxLength(300)]
        public string RegionImageURL { get; set; }
    }
}
