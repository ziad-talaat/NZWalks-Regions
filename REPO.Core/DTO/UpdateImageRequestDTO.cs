using Microsoft.AspNetCore.Http;
using REPO.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.DTO
{
    public  class UpdateImageRequestDTO
    {
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "File name is required.")]
        [StringLength(100, ErrorMessage = "File name cannot exceed 100 characters.")]
        public string FileName { get; set; }
        [StringLength(500, ErrorMessage = "File description cannot exceed 500 characters.")]
        public string? FileDescription { get; set; }
  
    

        public Guid? WalkId { get; set; }
        public Guid? RegionId { get; set; }







      

    }

    public static class ImageConvertor
    {
        public static void ConvertToImage(this Image image,UpdateImageRequestDTO request)
        {

            image.File = request.File;
            image.FileName = request.FileName;
            image.FileDescription = request.FileDescription;
            image.FileExtension = Path.GetExtension(request.File.FileName);
            image.FileSizeInBytes = request.File.Length;

            if (request.WalkId != null)
            {
                image.WalkId = request.WalkId;
            }

            if (request.RegionId != null)
            {
            image.RegionId = request.RegionId;
            }
            
        }
    }

}
