using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Api.Extensions;
using AspNetCorePropertyPro.Api.Resources.Propertys;
using AspNetCorePropertyPro.Configuration.Utils;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using AspNetCorePropertyPro.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePropertyPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotosController : ControllerBase
    {
        private readonly IPropertyImageService _propertyImageService;
        private readonly IPropertyService _propertyService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        private readonly IResponse _response;

        public PhotosController(IPropertyImageService propertyImageService, 
            IPropertyService propertyService,
            ICloudinaryService cloudinaryService,
            IMapper mapper,
            IResponse response)
        {
            _propertyImageService = propertyImageService;
            _propertyService = propertyService;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
            _response = response;
        }

        [HttpPost("{propertyId}")]
        public async Task<IActionResult> UploadImage(int propertyId, [FromForm] SavePropertyImageResource imageResource)
        {
            if (imageResource.File == null)
                return BadRequest(_response.Error("The file can't be null"));

            var property = await _propertyService.GetPropertById(propertyId);

            if (property.OwnerId != User.GetUserId())
                return BadRequest(_response.Error("You can't performed this action"));

            var imageUploadResult = await _cloudinaryService.UploadPhoto(imageResource.File);
            var propertyImage = await _propertyImageService.SavePropertyImage(propertyId, imageUploadResult);

            if (!await _propertyImageService.CheckPropertyHasMainImage(propertyId))
                propertyImage = await _propertyImageService.ToggleMainImageState(propertyImage);

            var propertyImageResource = _mapper.Map<PropertyImageResource>(propertyImage);

            return Ok(_response.Ok(propertyImageResource));
        }

        [HttpPatch("{id}/property/{propertyId}")]
        public async Task<IActionResult> SetMainPhoto(int propertyId, int id)
        {
            
            var property = await _propertyService.GetPropertById(propertyId);

            if (property.OwnerId != User.GetUserId())
                return BadRequest(_response.Error("You can't performed this action"));

            var mainPhoto = await _propertyImageService.GetPropertyMainImage(propertyId);
            var propertyImage = await _propertyImageService.GetImageById(id);

            await _propertyImageService.ToggleMainImageState(mainPhoto);
            propertyImage = await _propertyImageService.ToggleMainImageState(propertyImage);

            var propertyImageResource = _mapper.Map<PropertyImageResource>(propertyImage);

            return Ok(_response.Ok(propertyImageResource));
        }

        [HttpDelete("{id}/property/{propertyId}")]
        public async Task<IActionResult> DeletePhoto(int propertyId, int id)
        {

            var property = await _propertyService.GetPropertById(propertyId);

            if (property.OwnerId != User.GetUserId())
                return BadRequest(_response.Error("You can't performed this action"));

            var propertyImage = await _propertyImageService.GetImageById(id);
            
            if(propertyImage.IsMain)
                return BadRequest(_response.Error("You can't delete the property main photo"));

            var deletionResult = await _cloudinaryService.DeletePhoto(propertyImage.Public_Id);

            if (deletionResult.Result == "ok")
                await _propertyImageService.DeletePropertyImage(propertyImage);
           

            return NoContent();
        }
    }
}