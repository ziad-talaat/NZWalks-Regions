using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.Models
{
    public class Walk
    {

        [Key]
        public Guid Id { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(1000)]
        public string Description { get; set; }
        [Required,MaxLength(100)]
        public double LengthInKM { get; set; }
        [Required, MaxLength(300)]
        public string WalkingImageURL { get; set; }

        public Guid  RegionId { get; set; }
        public int DifficultyId{ get; set; }


        [ForeignKey(nameof(Walk.RegionId))]
        public Region Region { get; set; }
        [ForeignKey(nameof(Walk.DifficultyId))]
        public Difficulty Difficulty { get; set; }
    }
}
