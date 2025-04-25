using Microsoft.AspNetCore.Http;
using REPO.Core.Models;


namespace REPO.Core.DTO
{
    public class ImageToAdd
    {
        public IFormFile File { get; set; }
        //public Guid Id { get; set; }=Guid.NewGuid();
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public Guid? WalkId { get; set; }
        public Guid? RegionId { get; set; }

        public static Image ConvrtToImage(ImageToAdd request)
        {
            return new Image

            {
                File=request.File,
                FileDescription = request.FileDescription,
                FileName = request.FileName,
                FileExtension = Path.GetExtension(request.File.FileName),
                FileSizeInBytes=request.File.Length,
                WalkId=request.WalkId,
                RegionId=request.RegionId,
            };


        }

    }
}
