using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TP24.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/Error")]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception is ArgumentException)
                return BadRequest(exception.Message);
            else if (exception is ArgumentNullException)
                return BadRequest(exception.Message);
            else
                return StatusCode(500);
        }
    }
}
