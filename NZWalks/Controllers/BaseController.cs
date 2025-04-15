using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZ.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected List<string> ProjectTherErrors()
        {
            return ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage).ToList();
        }
    }
}
