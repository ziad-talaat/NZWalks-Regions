using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.Models
{
    public class Region
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        [Required]
        public string Code{ get; set; }
        [Required, MaxLength(300)]
        public string RegionImageURL{ get; set; }
    }
}
