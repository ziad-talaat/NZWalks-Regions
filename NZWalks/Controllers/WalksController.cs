using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REPO.Core.Contract;
using REPO.Core.DTO;
using REPO.Core.Models;
using REPO.EF;
using System.Data;

namespace NZ.Walks.Controllers
{
   
    public class WalksController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WalksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;   
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
           IEnumerable<Walk> walks = await _unitOfWork.Walk.GetAllAsync(new[] { "Difficulty","Region" });

            List<WalkResponseDTO> responseDTO=_mapper.Map<List<WalkResponseDTO>>(walks);
            

            return Ok(responseDTO);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetByID(Guid id)
        {
            Walk? walk = await _unitOfWork.Walk.GetByIdAsync(id);
            if (walk==null)
            {
                return NotFound("No such Walk");
            }

            WalkResponseDTO response = _mapper.Map<WalkResponseDTO>(walk);

            return Ok(response);
        } 

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create([FromBody]WalkRequestDTO walkReqDTO) 
        {
            if (ModelState.IsValid)
            {
               var walk=  _mapper.Map<Walk>(walkReqDTO);
               await _unitOfWork.Walk.CeateAsync(walk);
                await _unitOfWork.CompleteAsync();

                //map
                WalkResponseDTO response=_mapper.Map<WalkResponseDTO>(walk);

                return CreatedAtAction(nameof(GetByID), new { id = walk.Id }, response);
            }
            List<string> errors = ProjectTherErrors();
            return BadRequest(errors);
        }



        [HttpPut("{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult>Update(Guid id,WalkRequestDTO walkRequest)
        {
            Walk? walk = await _unitOfWork.Walk.GetByIdAsync(id);
            if(walk==null||!ModelState.IsValid)
            {
                var errors=ProjectTherErrors();
                return BadRequest(errors);
            }

            walk=_mapper.Map(walkRequest,walk);

           await  _unitOfWork.Walk.UpdateAsync(walk);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch(DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("FOREIGN KEY") == true)
                {
                    return BadRequest("Invalid foreign key value. Please ensure DifficultyId and RegionId exist.");
                }
            }
            WalkResponseDTO responseDTO=_mapper.Map<WalkResponseDTO>(walk);

            return Ok(responseDTO);
        }



        [HttpDelete("{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            Walk? walk = await _unitOfWork.Walk.DeleteAsync(id);
            if (walk == null)
            {
                return NotFound("No such walk");
            }
            await _unitOfWork.CompleteAsync();
            // RegionDTO Rdto = region.ConvertToRegionDTO();
            WalkResponseDTO Walkdto = _mapper.Map<WalkResponseDTO>(walk);
            return Ok(Walkdto);
        }





          [HttpGet("{walkDiff:alpha}")]
          public async  Task<IActionResult> GetWalksByDifficultyLevel(string walkDiff)
          {
       
              IEnumerable<Walk> walks = await _unitOfWork.Walk.GetFilteredAsync(x => x.Difficulty.Name == walkDiff, new[] { "Difficulty", "Region" });
       
              List<WalkResponseDTO> walkResponse = _mapper.Map<List<WalkResponseDTO>>(walks);
       
              return Ok(walkResponse);
          }
       
        [HttpGet("orderd/{walkDiff:alpha}")]
          public async  Task<IActionResult> GetWalksByDifficultyLevelOrderedByName(string walkDiff,[FromQuery]bool ordering)
          {
       
              IEnumerable<Walk> walks = await _unitOfWork.Walk.GetFiltered_OrderedAsync
                  (x => x.Difficulty.Name == walkDiff, x=>x.Name, new[] { "Difficulty", "Region" }, ordering);
       
              List<WalkResponseDTO> regionsDTO = _mapper.Map<List<WalkResponseDTO>>(walks);
       
              return Ok(regionsDTO);
          }

       //  [HttpGet("orderdByRef/{walkDiff:alpha}")]
       //   public async  Task<IActionResult> GetWalksByDifficultyLevelOrderedByName(string walkDiff, [FromQuery]string filterval)
       //   {
       //
       //     IEnumerable<Walk> walks = await _unitOfWork.Walk.GetFilteredAsyncUsingReflecton
       //       (filterProperty: walkDiff, filterValue: filterval, new[] { "Difficulty", "Region" });
       //
       //       List<WalkResponseDTO> regionsDTO = _mapper.Map<List<WalkResponseDTO>>(walks);
       //
       //       return Ok(regionsDTO);
       //   }




    }
}
