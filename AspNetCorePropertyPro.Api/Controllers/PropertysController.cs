using System.Linq;
using AspNetCorePropertyPro.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePropertyPro.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PropertysController : ControllerBase
    {

        public PropertysController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}