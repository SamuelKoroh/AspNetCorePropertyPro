using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface ITenantService
    {
        Task<Tenant> CreateTenant(Tenant tenant);
        Task<Tenant> GetTenant(string id);
        Task<IEnumerable<Tenant>> GetTenants();

    }
}
