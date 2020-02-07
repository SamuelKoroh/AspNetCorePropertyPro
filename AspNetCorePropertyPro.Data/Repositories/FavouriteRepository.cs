using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Data.Repositories
{
    public class FavouriteRepository : Repository<Favourite>, IFavouriteRepository
    {
        public FavouriteRepository(TenantDbContext context) : base(context)
        {
        }

        public Task<Favourite> GetWithUserPropertyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Favourite>> GetWithUserPropertyByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public TenantDbContext TenantDbContext 
        {
            get { return Context as TenantDbContext; }
        }
    }
}
