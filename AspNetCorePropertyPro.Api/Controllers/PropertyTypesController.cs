using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Api.Resources.PropertyType;
using AspNetCorePropertyPro.Configuration.Utils;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePropertyPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyTypesController : ControllerBase
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IMapper _mapper;
        private readonly IResponse _response;
        public PropertyTypesController(IPropertyTypeService propertyTypeService, IMapper mapper, IResponse response)
        {
            _mapper = mapper;
            _propertyTypeService = propertyTypeService;
            _response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetPropertyTypes()
        {
            var propertyTypes = await _propertyTypeService.GetAllPropertTypes();
            var propertyTypeResource = _mapper.Map<IEnumerable<PropertyTypeResource>>(propertyTypes);

            return Ok(_response.Ok(propertyTypeResource));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyType(int id)
        {
            var propertyType = await _propertyTypeService.FindPropertyTypeById(id);

            if (propertyType == null)
                return NotFound(_response.Error("The property type does not exists"));

            var propertyTypeResource = _mapper.Map<PropertyTypeResource>(propertyType);

            return Ok(_response.Ok(propertyTypeResource));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropertyType([FromBody] SavePropertyTypeResource resource)
        {
            var propertyType = await  _propertyTypeService.FindPropertyTypeByName(resource.Name);

            if (propertyType != null) 
                return BadRequest(_response.Error("The property type already exists"));

            propertyType = _mapper.Map<PropertyType>(resource);

            var result = await _propertyTypeService.CreatePropertyType(propertyType);
            var propertyTypeResource = _mapper.Map<PropertyTypeResource>(result);

            return Ok(_response.Ok(propertyTypeResource));
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePropertyType([FromBody] SavePropertyTypeResource resource, int id)
        {
            var propertyTypeToUpdate = await _propertyTypeService.FindPropertyTypeById(id);

            if (propertyTypeToUpdate == null)
                return NotFound(_response.Error("The property type does not exists"));

            var propertyType = _mapper.Map<PropertyType>(resource);
            var result = await _propertyTypeService.UpdatePropertyType(propertyTypeToUpdate, propertyType);
            var propertyTypeResource = _mapper.Map<PropertyTypeResource>(result);

            return Ok(_response.Ok(propertyTypeResource));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyType(int id)
        {
            var propertyType = await _propertyTypeService.FindPropertyTypeById(id);

            if (propertyType == null)
                return NotFound(_response.Error("The property type does not exists"));

            await _propertyTypeService.DeletePropertyType(propertyType);

            return NoContent();
        }
    }
}