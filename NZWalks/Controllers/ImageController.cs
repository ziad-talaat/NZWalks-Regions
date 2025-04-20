using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REPO.Core.Contract;
using REPO.Core.DTO;
using REPO.Core.Models;

namespace NZ.Walks.Controllers
{
   
    public class ImageController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        public ImageController(IUnitOfWork unitofwork, IImageService imageService)
        {
            _unitOfWork=unitofwork;
            _imageService=imageService;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imageModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };

                  await  _imageService.Upload(imageModel);
                return Ok(imageModel);
            }

            var errors = ProjectTherErrors();
            return BadRequest(errors);
        }






        [NonAction]
        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new string[] { ".jpg", ".png" };

            string extension = Path.GetExtension(request.File.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("file", "unsported File Extension");
            }

            if (request.File.Length > (long)(10 * 1024 * 1024))
            {
                ModelState.AddModelError("file", "file size is greater than 10mb");
            }
           
        }

    }
}
