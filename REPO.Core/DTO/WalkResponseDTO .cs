using REPO.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.DTO
{
    public class WalkResponseDTO  
    {
        public string Name { get; set; }
        [Required, MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [Range(0, 999.99)]
        public double LengthInKM { get; set; }
        [Required, MaxLength(300)]
        public string WalkingImageURL { get; set; }

       
        public string ?RegionName { get; set; }
        public string? DifficultyName { get; set; }

    }
}
