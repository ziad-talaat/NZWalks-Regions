using Microsoft.EntityFrameworkCore;
using NZ.Walks.Controllers;
using REPO.Core.Contract;
using REPO.Core.DTO;
using REPO.Core.Models;
using REPO.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.EF.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IIMageRepository
    {
        public ImageRepository(AppDbContext context) : base(context)
        {

        }

        public async  Task<ImageUploadRequestDTO?> GetImageDetailsAsync(Guid id)
        {
            return await _context.Images.Where(x => x.Id == id).Select(x => new ImageUploadRequestDTO
            {
                FileName = x.FileName,
                FileDescription = x.FileDescription,
                FileExtension = x.FileExtension,
                FilePath = x.FilePath,
                FileSize=x.FileSizeInBytes,
                WalkName = x.Walk.Name,
                RegionName = x.Region.Name


            }).FirstOrDefaultAsync();
        }
               



       
    }
}




