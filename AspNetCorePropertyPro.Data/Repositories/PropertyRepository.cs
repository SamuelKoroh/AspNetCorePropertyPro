using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Data.Repositories
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(TenantDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Property>> GetAllWithUserAsync()
        {
            return await TenantDbContext.Properties
                .Include(x => x.PropertyImages)
                .Include(x => x.Owner).ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllWithOwnerByOwnerIdAsync(string ownerId)
        {
            return await TenantDbContext.Properties
                .Include(x => x.PropertyImages)
                .Include(x => x.Owner)
                .Where(x => x.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<Property> GetWithOwnerAsync(int id)
        {
            return await TenantDbContext.Properties
                .Include(x => x.PropertyImages)
                .Include(x => x.Owner)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        private TenantDbContext TenantDbContext
        {
            get { return Context as TenantDbContext; }
        }
    }
}
