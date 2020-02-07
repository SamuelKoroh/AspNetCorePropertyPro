using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Api.Resources.Flags;
using AspNetCorePropertyPro.Configuration.Utils;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePropertyPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlagsController : ControllerBase
    {
        private readonly IFlagService _flagService;
        private readonly IMapper _mapper;
        private readonly IResponse _response;

        public FlagsController(IFlagService flagService, IMapper mapper, IResponse response)
        {
            _flagService = flagService;
            _mapper = mapper;
            _response = response;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlag([FromBody] FlagSaveEditResource resource)
        {
            var flag = _mapper.Map<Flag>(resource);

            flag = await _flagService.CreateFlag(flag);

            return CreatedAtRoute(flag.Id, flag); ;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlag(int id)
        {

            var flag = await _flagService.GetFlagWithProperty(id);
            
            if(flag == null)
                return NotFound(_response.Error("The flag does not exist"));

            var flagResource = _mapper.Map<FlagResource>(flag);

            return Ok(_response.Ok(flagResource));
        }

        [HttpGet]
        public async Task<IActionResult> GetFlags()
        {

            var flags = await _flagService.GetFlags();

            var flagResource = _mapper.Map<IEnumerable<FlagResource>>(flags);

            return Ok(_response.Ok(flagResource));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlag(int id)
        {

            var flag = await _flagService.GetFlag(id);

            if (flag == null)
                return NotFound(_response.Error("The flag does not exist"));

             _flagService.DeleteFlag(flag);

            return NotFound();
        }
    }
}