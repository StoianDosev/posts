using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InitializeController : ControllerBase
    {
        private readonly IJsonObjectsInitializer _initializer;

        public InitializeController(IJsonObjectsInitializer initializer)
        {
            _initializer = initializer;
        }

        [HttpGet()]
        public async Task<IActionResult> Reload()
        {
            await _initializer.Reload();
            return new JsonResult(new { message = "reloaded" });
        }
    }
}