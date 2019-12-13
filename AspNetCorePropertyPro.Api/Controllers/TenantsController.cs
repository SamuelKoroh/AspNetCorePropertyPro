using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePropertyPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly GlobalDbContext globalDbContext;

        public TenantsController(GlobalDbContext globalDbContext)
        {
            this.globalDbContext = globalDbContext;
        }
        [HttpPost("register")]
        public IActionResult Register()
        {
            var tenant = globalDbContext.Tenants.Add(new Core.Models.Tenant
            {
                Name = "tenant1",
                HostName = "tenant1",
                ConnectionString = "PPTenantOne"
            });
            globalDbContext.SaveChanges();
            return Ok(tenant);
        }
    }
}