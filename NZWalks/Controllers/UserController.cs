using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using REPO.Core.Consts;
using REPO.Core.Contract;
using REPO.Core.DTO;
using REPO.Core.Models;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace NZ.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserController> _logger;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IJwtService jwtService, UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IUnitOfWork unitOfWork, ILogger<UserController> logger, RoleManager<IdentityRole> roleManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork= unitOfWork;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<string> errors = ProjectTherErrors();
                    return BadRequest(errors);
                }


                ApplicationUser? existingUserEmail = await _userManager.FindByEmailAsync(register.Email);
                if (existingUserEmail != null)
                {
                    return BadRequest("Email is already taken.");
                }


                ApplicationUser user = new ApplicationUser()
                {
                    Email = register.Email,
                    UserName = register.Name,
                    Address = register.Address,

                };


                IdentityResult result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Reader);
                    var authResponse = await  _jwtService.CreateJwtToken(user);
                    user.RefrehToken = authResponse.RefreshToken;
                    user.ExpirationRefreshToken = authResponse.RF_Expiration;
                    await _userManager.UpdateAsync(user);
                    return Ok(authResponse);
                }

                return BadRequest(result.Errors.Select(e => e.Description));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                // Return a generic error message to the client
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }





        [HttpPost("login")]
        public async Task<IActionResult> Login( [FromBody]LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                List<string>erors= ProjectTherErrors();
                return BadRequest(erors);
            }

            ApplicationUser? user = await _unitOfWork.AppUser.GetUserByEmail(login.Email);
            if (user == null)
            {
                return BadRequest("no such email");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password,false);

            if (result.IsLockedOut)
            {
                return BadRequest("Account is locked.");
            }

            if (result.Succeeded)
            {
                var authReponse =  await _jwtService.CreateJwtToken(user);
                user.RefrehToken = authReponse.RefreshToken;
                user.ExpirationRefreshToken = authReponse.RF_Expiration;
                await _userManager.UpdateAsync(user);
                return Ok(authReponse);
            }
            return BadRequest("password not correct or other error.");
        }


      [HttpPost("assignToRole")]
      [Authorize(Roles=nameof(Roles.Admin))]
      public async Task<IActionResult> AssignUserToRole([FromBody]AssignToRole assignDTO)
      {
          ApplicationUser? user = await _unitOfWork.AppUser.GetUserByNormalizdName(assignDTO.UserName);
          if (user == null) 
              return NotFound($"User with name  {assignDTO.UserName} not found.");
          
          
            bool isRoleExist = await _roleManager.RoleExistsAsync(assignDTO.RoleName);

            if (!isRoleExist)
                return NotFound($"No such role ( {assignDTO.RoleName}) ");


            var currentRoles=await _userManager.GetRolesAsync(user);

            if (currentRoles.Contains(assignDTO.RoleName) && currentRoles.Count == 1)
            {
                return Ok($"User {assignDTO.UserName} is already in role {assignDTO.RoleName}");
            }


            var removingOldRoles = await _userManager.RemoveFromRolesAsync(user,currentRoles);

            if (!removingOldRoles.Succeeded)
            {
                var errors = string.Join(", ", removingOldRoles.Errors.Select(e => e.Description));
                return Problem($"Couldn't remove old roles: {errors}");
            }

            
            var result = await _userManager.AddToRoleAsync(user, assignDTO.RoleName);


            if (result.Succeeded)
            {
                

                return Ok($"user {assignDTO.UserName} assigned to role {assignDTO.RoleName} successfully");

            }
            return BadRequest(result.Errors.Select(e => e.Description));
        }



        [HttpPost("generate-RefreshToken")]
        public async Task<IActionResult> GenerateRefreshToken(TokenModel model)
        {
            if (model == null)
                return BadRequest("");

            ClaimsPrincipal? principle = _jwtService.GetPrinciplesFromJWTToken(model.Token);
            if (principle == null)
                return BadRequest("");

            string? id = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ApplicationUser? user =await _userManager.FindByIdAsync(id);
            if (user == null || user.RefrehToken != model.RefreshToken || user.ExpirationRefreshToken < DateTime.Now)
                return BadRequest("invalid operation");

            AuthanticatedResponse auth =  await _jwtService.CreateJwtToken(user);
            user.RefrehToken = auth.RefreshToken;
            user.ExpirationRefreshToken = auth.RF_Expiration;
            await _userManager.UpdateAsync(user);

            return Ok(auth);
        }

     
    }
}
