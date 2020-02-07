using AspNetCorePropertyPro.Core;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Services
{
    public class TenantService : ITenantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TenantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Tenant> CreateTenant(Tenant tenant)
        {
            await _unitOfWork.Tenants.AddAsync(tenant);
            await _unitOfWork.CommitAsync();
            return tenant;
        }

        public async Task<Tenant> GetTenant(string id)
        {
            return await _unitOfWork.Tenants.SingleorDefaultAsync(t => t.Id.ToString() == id);
        }

        public async Task<IEnumerable<Tenant>> GetTenants()
        {
            return await _unitOfWork.Tenants.GetAllAsync();
        }
    }
}
