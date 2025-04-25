using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REPO.Core.Contract;
using REPO.Core.DTO;
using REPO.Core.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NZ.Walks.Controllers
{

    public class ImageController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        public ImageController(IUnitOfWork unitofwork, IImageService imageService)
        {
            _unitOfWork = unitofwork;
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(Guid id)
        {
            
                ImageUploadRequestDTO? imageDTO = await _unitOfWork.Image.GetImageDetailsAsync(id);

                if (imageDTO == null)

                    return NotFound("No such image");

                return Ok(imageDTO);
        }

     

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageToAdd request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {

               
                Walk? walk = await _unitOfWork.Walk.GetByIdAsync(x => x.Id == request.WalkId, new[] { "Region" });
                if (walk == null)
                    return NotFound("Walk not found.");

                if(walk.RegionId!=request.RegionId)
                {
                    return BadRequest("Walk not belong to the specified region");
                }

                Image imageModel=ImageToAdd.ConvrtToImage(request);

                await _imageService.Upload(imageModel);
                await _unitOfWork.Image.CeateAsync(imageModel);
                await _unitOfWork.CompleteAsync();

                ImageUploadRequestDTO dto = ImageUploadRequestDTO.ConvrtToImageDTO(imageModel,walk.Region.Name,walk.Name);
                return CreatedAtAction(nameof(GetImage), new {id=imageModel.Id}, dto);
            }
            var errors = ProjectTherErrors();
            return BadRequest(errors);
        }



        [HttpGet]
        public async  Task<IActionResult> GetAllImages()
        {
           var images = await  _unitOfWork.Image.GetAllAsync(new[] { "Walk", "Region" });
            List<ImageUploadRequestDTO> response = images.Select(x => ImageUploadRequestDTO.ConvrtToImageDTO(x,x.Region?.Name,x.Walk?.Name)).ToList();
            return Ok(response);
        }


        ///update Image

        [HttpGet("Name/{walkName}")]
        public async Task<IActionResult> GetAllInWalk(string walkName)
        {
            var response= await _unitOfWork.Image.GetAllAsync(x =>x.Walk.Name==walkName , x=> ImageUploadRequestDTO.ConvrtToImageDTO(x,x.Region.Name,x.Walk.Name));
           
            return Ok(response);
        }
          [HttpGet("regionName/{regionName}")]
        public async Task<IActionResult> GetAllInRegion(string regionName)
        {
            var response= await _unitOfWork.Image.GetAllAsync(x =>x.Region.Name==regionName , x=> ImageUploadRequestDTO.ConvrtToImageDTO(x,x.Region.Name,x.Walk.Name));
           
            return Ok(response);
        }






        [HttpDelete]
        public async Task<IActionResult> DeleteImage(Guid id)
        { 
           Image? image= await _unitOfWork.Image.DeleteAsync(id);
            if(image == null)
            {
                return NotFound($"no image with this id:{id}");
            }
            await _unitOfWork.CompleteAsync();


            return Ok(new
            {
                Message = "Image deleted successfully",
                DeletedImageId = image.Id
            });
        }



        [NonAction]
        private void ValidateFileUpload(ImageToAdd request)
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
