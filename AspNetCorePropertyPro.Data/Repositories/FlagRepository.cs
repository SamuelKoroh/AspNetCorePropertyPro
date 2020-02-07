using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Data.Repositories
{
    public class FlagRepository : Repository<Flag>, IFlagRepository
    {
        public FlagRepository(TenantDbContext context) : base(context)
        {
        }

        public TenantDbContext TenantDbContext
        {
            get { return Context as TenantDbContext; }
        }

        public async Task<IEnumerable<Flag>> GetAllFlagsWithProperty()
        {
            return await TenantDbContext.Flags.Include(f => f.Property)
                .ThenInclude(p => p.Owner).ToListAsync();
        }

        public async Task<Flag> GetFlagWithPropertyById(int id)
        {
            return await TenantDbContext.Flags.Include(f => f.Property)
                .ThenInclude(p => p.Owner).SingleOrDefaultAsync(f => f.Id ==id);
        }
    }
}
