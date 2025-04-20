using REPO.Core.Contract;
using REPO.Core.Models;

namespace NZ.Walks.Controllers
{
    public interface IUserRepository:IBaseRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetUserByEmail(string email);
    }
}
