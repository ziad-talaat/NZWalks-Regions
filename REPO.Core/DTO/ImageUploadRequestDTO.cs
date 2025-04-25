using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using REPO.Core.Models;
using System.ComponentModel.DataAnnotations;


namespace REPO.Core.DTO
{
    public class ImageUploadRequestDTO
    {
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string? FileExtension   { get; set; }
        public string? FilePath   { get; set; }
        public long? FileSize   { get; set; }
        

        public string? WalkName { get; set; }
        public string ?RegionName { get; set; }



        public static ImageUploadRequestDTO ConvrtToImageDTO(Image request,string? regionName=null,string ?walkName=null)
        {
            return new ImageUploadRequestDTO
            {
                FileExtension = request.FileExtension,
                FileName = request.FileName,
                FileDescription = request.FileDescription,
                FilePath=request.FilePath,
                FileSize=request.FileSizeInBytes,
                RegionName=regionName,
                WalkName=walkName
            };


        }
         public static Image ConvrtToImage(ImageUploadRequestDTO request)
        {
            return new Image

            { 
               FileDescription=request.FileDescription,
               FileName=request.FileName,
               FileExtension=request.FileExtension,
               FilePath=request.FilePath
            };


        }


    }
}
