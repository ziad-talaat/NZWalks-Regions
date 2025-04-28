using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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



        [HttpPut]
        public async Task<IActionResult> UpdateImage(Guid id, UpdateImageRequestDTO request)
        {

            if (!string.IsNullOrEmpty(request.RegionId.ToString()))
            {
                Region? region = await _unitOfWork.Region.GetByIdAsync(request.RegionId ?? Guid.Empty);
                if (region == null)
                    return BadRequest("no such region ");
            }
            if (!string.IsNullOrEmpty(request.WalkId.ToString()))
            {
                Walk? walk = await _unitOfWork.Walk.GetByIdAsync(request.WalkId ?? Guid.Empty);
                if ( walk == null)
                    return BadRequest("no such  walk");
            }
           

            if (ModelState.IsValid)
            {
                using var transaction = await _unitOfWork.Image.BeginTransactionAsync();
                try
                {
                    Image? image = await _unitOfWork.Image.GetByIdAsync(id);
                    if (image == null)
                        return NotFound("No such Image");

                    image.ConvertToImage(request);

                    image.FilePath = _imageService.GetfilePath(image.FileName, Path.GetExtension(image.File.FileName));

                   
                    await _imageService.Upload(image);
                    await _unitOfWork.Image.UpdateAsync(image);
                    await _unitOfWork.CompleteAsync();
                    await transaction.CommitAsync();
                    return Ok("Done");
                }
                catch (DbUpdateException ex)
                {
                    await transaction.RollbackAsync();  
                    return BadRequest($"there an error:{ex.Message}");
                }

            }
            var errors = ProjectTherErrors();
            return BadRequest(errors);
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
