using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using REPO.Core.Contract;
using REPO.Core.DTO;
using REPO.Core.Models;

namespace NZ.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IJwtService jwtService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            _jwtService = jwtService;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork= unitOfWork;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO register)
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


            IdentityResult result=await _userManager.CreateAsync(user,register.Password);
            if (result.Succeeded)
            {
                var authResponse = _jwtService.CreateJwtToken(user);
                return Ok(authResponse);
            }

            return BadRequest(result.Errors);
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
                var authReponse = _jwtService.CreateJwtToken(user);
                return Ok(authReponse);
            }
             
               
            return BadRequest("password not correct or other error.");

        }
    }
}
