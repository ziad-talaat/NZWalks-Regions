using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.DTO
{
    public class RegionDTO
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
