using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Api.Extensions;
using AspNetCorePropertyPro.Api.Resources.Users;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IResponse _response;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IResponse response, IMapper mapper)
        {
            _userService = userService;
            _response = response;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            var userResources = _mapper.Map<IEnumerable<UserResource>>(users);

            return Ok(_response.Ok(userResources));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userService.GetUserByID(userId);

            if (user ==null)
                return NotFound(_response.Error("The user was not found"));

            var userResource = _mapper.Map<UserResource>(user);

            return Ok(_response.Ok(userResource));
        }

        [HttpGet("/me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userService.GetUserByID(User.GetUserId());
            var userResource = _mapper.Map<UserResource>(user);

            return Ok(_response.Ok(userResource));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserResource resource)
        {
            var user = _mapper.Map<ApplicationUser>(resource);
            var userToUpdate = await _userService.GetUserByID(User.GetUserId());

            if(userToUpdate ==null)
                return NotFound(_response.Error("The user was not found"));

            userToUpdate = await _userService.UpdateUser(userToUpdate, user);

            var userResource = _mapper.Map<UserResource>(userToUpdate);

            return Ok(_response.Ok(userResource));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userService.GetUserByID(userId);

            if (user == null)
                return NotFound(_response.Error("The user was not found"));

            await _userService.DeleteUser(user);

            return NoContent();
        }
    }
}