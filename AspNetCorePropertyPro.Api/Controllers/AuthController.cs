using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Api.Extensions;
using AspNetCorePropertyPro.Api.Resources;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePropertyPro.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        /// <summary>
        /// Register new site user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /register
        ///     {
        ///         "firstName": "john",
        ///         "lastName":"doe",
        ///         "address": "address of john doe",
        ///         "phone": "2348098585647",
        ///         "email": "johndoe@mail.com",
        ///         "password": "Password@13493"
        ///     }
        ///     
        /// </remarks>
        /// <param name="registerResource"></param>
        /// <returns>success</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterResource registerResource)
        {
            var user = _mapper.Map<ApplicationUser>(registerResource);
            var result = await _authService.RegisterAsync(user, registerResource.Password);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result.Message);
        }

        [HttpPost("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailResource confirmEmailResource)
        {
            var result = await _authService.ConfirmEmailAsync(confirmEmailResource.UserId, confirmEmailResource.Code);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result.Message);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginResource loginResource)
        {
            var result = await _authService.LoginAsync(loginResource.Email, loginResource.Password);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("forget-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordResource resource)
        {
            var result = await _authService.ForgetPasswordAsync(resource.Email);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordResource resource)
        {
            var result = await _authService.ResetPasswordAsync(resource.UserId, resource.Code, resource.Password);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordResource resource)
        {
            var userId = User.GetUserId();

            var result = await _authService.ChangePasswordAsync(userId, resource.OldPassword, resource.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
    }
}