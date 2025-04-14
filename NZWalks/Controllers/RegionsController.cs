using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using REPO.Core.Contract;
using REPO.Core.DTO;
using REPO.Core.Models;

namespace NZ.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        public RegionsController(IUnitOfWork unitofWork,IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Region> regions = await _unitofWork.Region.GetAllAsync();
            //IEnumerable<RegionDTO> regionsDTOList = Helper.ConvertToRegionsDTO(regions.ToList());

            var regionsDTOList=_mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionsDTOList);
        }



        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Region? region = await _unitofWork.Region.GetByIdAsync(id);

            if (region != null)
            {
                // RegionDTO regionDTO = region.ConvertToRegionDTO();

                RegionDTO regionDTO= _mapper.Map<RegionDTO>(region);

                return Ok(regionDTO);
            }

            return NotFound("No such Region");
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegionDTO regionDTO)
        {
            if (ModelState.IsValid)
            {

                // Region region = regionDTO.ConvertToRegion();

                Region region=_mapper.Map<Region>(regionDTO);
               await  _unitofWork.Region.CeateAsync(region);
                await _unitofWork.CompleteAsync();


                // RegionDTO regionDtoResponse = region.ConvertToRegionDTO();
                RegionDTO regionDtoResponse=_mapper.Map<RegionDTO>(region);

                return CreatedAtAction(nameof(GetById), new { id = region.Id }, regionDtoResponse);
            }
            List<string> errors = ProjectTherErrors();

            return BadRequest(errors);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Upadte(Guid id, [FromBody] UpdateRegionRequestDTO UpdateregionDTO)
        {
            Region? region = await _unitofWork.Region.GetByIdAsync(id);
            if (region == null || !ModelState.IsValid)
            {
                List<string> errors = ProjectTherErrors();

                return BadRequest(errors);
            }

            //  UpdateregionDTO.ConvertToUpdatedRegion(region);
            region = _mapper.Map(UpdateregionDTO, region); // Convert to region without creating new entity
           await _unitofWork.Region.UpdateAsync(region);
            await _unitofWork.CompleteAsync();



            //  RegionDTO regionDTo = region.ConvertToRegionDTO();
            RegionDTO regionDTo = _mapper.Map<RegionDTO>(region);

            return Ok(regionDTo);
        }



        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
              Region? region= await _unitofWork.Region.DeleteAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            await _unitofWork.CompleteAsync();
            // RegionDTO Rdto = region.ConvertToRegionDTO();
            RegionDTO Rdto = _mapper.Map<RegionDTO>(region);
            return Ok(Rdto);
        }




        [NonAction]
        private List<string> ProjectTherErrors()
        {
            return ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage).ToList();
        }


    }
}
