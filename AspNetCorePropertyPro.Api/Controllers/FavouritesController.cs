using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Api.Extensions;
using AspNetCorePropertyPro.Configuration.Utils;
using AspNetCorePropertyPro.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePropertyPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly IResponse _response;
        private readonly IFavouriteService _favouriteService;

        public FavouritesController(IFavouriteService favouriteService, IResponse response)
        {
            _favouriteService = favouriteService;
            _response = response;
        }

        [HttpPost("{propertyId}")]
        public async Task<IActionResult> AddFavorite(int propertyId)
        {
            var userId = User.GetUserId();

            if (await _favouriteService.FavouriteExists(userId, propertyId))
            {
                var userFavourite = await _favouriteService.GetFavourite(userId, propertyId);

                _favouriteService.RemoveFavourite(userFavourite);

                return Ok(_response.Ok("The property has been removed from your favourite list"));
            }

            var favourite = await _favouriteService.AddFavourite(userId, propertyId);
            
            return Ok(_response.Ok(favourite));
        }

        [HttpGet]
        public async Task<IActionResult> GetFavourites()
        {
            var userId = User.GetUserId();
            var favourites = await _favouriteService.GetUserFavourites(userId);

            return Ok(_response.Ok(favourites));
        }

        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> DeleteFavourite(int propertyId)
        {
            var userId = User.GetUserId();
            var userFavourite = await _favouriteService.GetFavourite(userId, propertyId);

            if (userFavourite == null)
                return BadRequest(_response.Error("This property is not in your favourite list"));

            _favouriteService.RemoveFavourite(userFavourite);

            return Ok(_response.Ok("The property has been removed from your favourite list"));
        }
    }
}