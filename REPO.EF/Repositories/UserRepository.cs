using Microsoft.EntityFrameworkCore;
using NZ.Walks.Controllers;
using REPO.Core.Models;
using REPO.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.EF.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(AppDbContext context):base(context)
        {
            
        }
        public async Task<ApplicationUser?> GetUserByEmail(string email)
        {
          return await  _context.ApplicationUser.FirstOrDefaultAsync(x=>x.Email== email);
        }

        public async Task<ApplicationUser?> GetUserByNormalizdName(string name)
        {
            return await _context.ApplicationUser.FirstOrDefaultAsync(x => x.NormalizedUserName == name.ToUpper());
        }
    }
}
