using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Core.Services;
using AspNetCorePropertyPro.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePropertyPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
         

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTenants()
        {
            var tenants = await _tenantService.GetTenants();

            return Ok(tenants);
        }
    }
}