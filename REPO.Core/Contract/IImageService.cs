using REPO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.Contract
{
    public interface IImageService
    {
        Task<Image> Upload(Image image);
         string GetfilePath(string fileName, string fileExtension);
    }
}
