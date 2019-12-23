using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Repositories
{
    public interface IFavouriteRepository : IRepository<Favourite>
    {
        Task<IEnumerable<Favourite>> GetWithUserPropertyByUserId(string userId);
        Task<Favourite> GetWithUserPropertyByIdAsync(int id);
    }
}
