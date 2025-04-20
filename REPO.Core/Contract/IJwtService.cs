using REPO.Core.DTO;
using REPO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.Contract
{
    public interface IJwtService
    {
        Task<AuthanticatedResponse> CreateJwtToken(ApplicationUser user);
    }
}
