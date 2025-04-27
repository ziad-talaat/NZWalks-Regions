using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using REPO.Core.Contract;
using REPO.Core.Models;


namespace REPO.EF.Service
{
    public class ImageService :  IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public ImageService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;   
        }




      
        public async Task<Image> Upload(Image image)
        { 

            //This gets the root directory of the application. It refers to the base directory where your application is running and compine with Images and then filename and fileExtension     ,,,,,   _webHostEnvironment.ContentRootPath=> gets the floder path of the base project here
              // ex) C:\path\to\app\Images\image.jpg
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");


            //Upload Image To localPath

           // This creates a new file stream that will write to the specified
            using var stream = new FileStream(localFilePath, FileMode.Create);  //takes the path to write and Enum ToWrite Option

            await image.File.CopyToAsync(stream); // write in the stream ('stream' that has the path where will write and write optionn)



            var urlFilePath = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}{_httpContextAccessor.HttpContext?.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

          
            return image;
        }
        public string GetfilePath(string fileName, string fileExtension)
        {
            return $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}{_httpContextAccessor.HttpContext?.Request.PathBase}/Images/{fileName}{fileExtension}";

        }
    }
}
