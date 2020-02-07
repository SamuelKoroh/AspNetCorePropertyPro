using AspNetCorePropertyPro.Core;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Services
{
    public class FavouriteService : IFavouriteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavouriteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Favourite> AddFavourite(string userId, int propertyId)
        {
            var favourite = new Favourite { PropertyId = propertyId, UserId = userId };

            await _unitOfWork.Favourites.AddAsync(favourite);
            await _unitOfWork.CommitAsync();

            return favourite;
        }

        public async Task<IEnumerable<Favourite>> GetUserFavourites(string userId)
        {
            return await _unitOfWork.Favourites.Find(f => f.UserId == userId);
        }

        public async Task<Favourite> GetFavourite(int favouriteId)
        {
            return await _unitOfWork.Favourites.SingleorDefaultAsync(f => f.Id == favouriteId);
        }
        public void RemoveFavourite(Favourite favourite)
        {
            _unitOfWork.Favourites.RemoveAsync(favourite);
            _unitOfWork.CommitAsync();
        }

        public async Task<bool> FavouriteExists(string userId, int propertyId)
        {
            return await _unitOfWork.Favourites.AnyAsync(f => f.PropertyId == propertyId && f.UserId == userId);
        }

        public async Task<Favourite> GetFavourite(string userId, int propertyId)
        {
            return await _unitOfWork.Favourites.SingleorDefaultAsync(f => f.PropertyId == propertyId && f.UserId == userId);
        }
    }
}
