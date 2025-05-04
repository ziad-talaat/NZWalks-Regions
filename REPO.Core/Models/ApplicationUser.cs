using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Address { get; set; }
        public string? RefrehToken { get; set; }
        public DateTime? ExpirationRefreshToken { get; set; }
    }
}
