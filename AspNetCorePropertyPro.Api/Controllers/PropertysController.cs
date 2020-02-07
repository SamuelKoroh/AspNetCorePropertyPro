using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Api.Extensions;
using AspNetCorePropertyPro.Api.Resources.Propertys;
using AspNetCorePropertyPro.Configuration.Utils;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePropertyPro.Api.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertysController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;
        private readonly IResponse _response;

        public PropertysController(IPropertyService propertyService, IMapper mapper, IResponse response)
        {
            _propertyService = propertyService;
            _mapper = mapper;
            _response = response;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var properties = await _propertyService.GetAllProperty();
            var propertyResources = _mapper.Map<IEnumerable<PropertyResource>>(properties);

            return Ok(_response.Ok(propertyResources));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var property = await _propertyService.GetPropertById(id);

            if (property == null)
                return BadRequest(_response.Error("The property does not exists"));

            var propertyResource = _mapper.Map<PropertyDetailResource>(property);

            return Ok(_response.Ok(propertyResource));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] SaveEditPropertyResource resource)
        {
            if (await _propertyService.CheckIfPropertyTitleExists(resource.Title))
                return BadRequest(_response.Error("Property with same title already exists"));

            var property = _mapper.Map<Property>(resource);

            property = await _propertyService.CreateProperty(property, User.GetUserId());

            var propertyResource = _mapper.Map<PropertyResource>(property);

            return Ok(_response.Ok(propertyResource));
        }

        [HttpPatch("{propertyId}")]
        public async Task<IActionResult> UpdateProperty(int propertyId, [FromBody] SaveEditPropertyResource resource)
        {
            var propertyToUpdate = await _propertyService.GetPropertById(propertyId);

            if (propertyToUpdate.OwnerId != User.GetUserId())
                return BadRequest(_response.Error("You can't performed this action"));

            var property = _mapper.Map<Property>(resource);

            propertyToUpdate = await _propertyService.UpdateProperty(propertyToUpdate, property);

            var propertyResource = _mapper.Map<PropertyResource>(propertyToUpdate);

            return Ok(_response.Ok(propertyResource));
        }

        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> DeleteProperty(int propertyId)
        {
            var property = await _propertyService.GetPropertById(propertyId);

            if (property.OwnerId != User.GetUserId())
                return BadRequest(_response.Error("You can't performed this action"));

            await _propertyService.DeleteProperty(property);

            return NoContent();
        }
    }
}