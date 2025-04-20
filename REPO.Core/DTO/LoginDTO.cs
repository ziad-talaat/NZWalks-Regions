using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace REPO.Core.DTO
{
    public class LoginDTO
    {
        
        [Required]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "not an email format")]
        public string Email { get; set; }
        
       
        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        


    }
}
