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
        public async Task<IActionResult> Register()
        {
            var tenant = new Core.Models.Tenant
            {
                Name = "tenant1",
                HostName = "tenant1",
                ConnectionString = "Server=tcp:aspnetpropertypro.database.windows.net,1433;Initial Catalog=Tenant1DB;Persist Security Info=False;User ID=propertyproadmin;Password=admin@2012;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
            };

            await globalDbContext.Tenants.AddAsync(tenant);
            await globalDbContext.SaveChangesAsync();

            return Ok(tenant);
        }
    }
}