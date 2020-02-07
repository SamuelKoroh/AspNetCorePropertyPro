using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IFavouriteService
    {
        Task<Favourite> AddFavourite(string userId, int propertyId);
        Task<IEnumerable<Favourite>> GetUserFavourites(string userId);
        Task<Favourite> GetFavourite(string userId, int propertyId);
        Task<Favourite> GetFavourite(int favouriteId);
        Task<bool> FavouriteExists(string userId, int propertyId);
        void RemoveFavourite(Favourite favourite);
    }
}
