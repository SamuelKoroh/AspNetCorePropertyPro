using System.Linq;
using System.Security.Claims;
using AspNetCorePropertyPro.Api.Extensions;
using AspNetCorePropertyPro.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePropertyPro.Api.Controllers
{

    [Authorize]
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
            var userId = User.GetUserId();
            return Ok(userId);
        }
    }
}