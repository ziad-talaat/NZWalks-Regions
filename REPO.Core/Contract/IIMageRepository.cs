using REPO.Core.DTO;
using REPO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.Contract
{
    public interface IIMageRepository:IBaseRepository<Image>
    {
        Task<ImageUploadRequestDTO?> GetImageDetailsAsync(Guid id);
       
    }
}
