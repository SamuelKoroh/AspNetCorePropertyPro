using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Api.Resources.DealType;
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
    public class DealTypesController : ControllerBase
    {
        private readonly IDealTypeService _dealTypeService;
        private readonly IMapper _mapper;
        private readonly IResponse _response;
        public DealTypesController(IDealTypeService dealTypeService, IMapper mapper, IResponse response)
        {
            _mapper = mapper;
            _dealTypeService = dealTypeService;
            _response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetDealTypes()
        {
            var dealTypes = await _dealTypeService.GetAllDealType();
            var dealTypeResources = _mapper.Map<IEnumerable<DealTypeResource>>(dealTypes);

            return Ok(_response.Ok(dealTypeResources));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDealType(int id)
        {
            var dealType = await _dealTypeService.FindDealTypeById(id);

            if (dealType == null)
                return NotFound(_response.Error("The deal type does not exists"));

            var dealTypeResource = _mapper.Map<DealTypeResource>(dealType);

            return Ok(_response.Ok(dealTypeResource));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropertyType([FromBody] SaveDealTypeResource resource)
        {
            var dealType = await _dealTypeService.FindDealTypeByName(resource.Name);

            if (dealType != null)
                return BadRequest(_response.Error("The deal type already exists"));

            dealType = _mapper.Map<DealType>(resource);

            var result = await _dealTypeService.CreateDealType(dealType);
            var dealTypeResource = _mapper.Map<DealTypeResource>(result);

            return Ok(_response.Ok(dealTypeResource));
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePropertyType([FromBody] SaveDealTypeResource resource, int id)
        {
            var dealTypeToUpdate = await _dealTypeService.FindDealTypeById(id);

            if (dealTypeToUpdate == null)
                return NotFound(_response.Error("The deal type does not exists"));

            var dealType = _mapper.Map<DealType>(resource);
            var result = await _dealTypeService.UpdateDealType(dealTypeToUpdate, dealType);
            var dealTypeResource = _mapper.Map<DealTypeResource>(result);

            return Ok(_response.Ok(dealTypeResource));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyType(int id)
        {
            var dealType = await _dealTypeService.FindDealTypeById(id);

            if (dealType == null)
                return NotFound(_response.Error("The deal type does not exists"));

            await _dealTypeService.DeleteDealType(dealType);

            return NoContent();
        }
    }
}