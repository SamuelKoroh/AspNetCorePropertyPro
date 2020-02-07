using AspNetCorePropertyPro.Configuration;
using AspNetCorePropertyPro.Configuration.Constants;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Response;
using AspNetCorePropertyPro.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AspNetCorePropertyPro.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IOptions<JwtSetting> _jwtSetting;

        public AuthService(UserManager<ApplicationUser> userManager, IEmailService emailService, IOptions<JwtSetting> jwtSetting)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtSetting = jwtSetting;
        }

        public async Task<BaseResponse> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new BaseResponse { Message = "The account doesn't exist!" };

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new BaseResponse { Message = "The account email doesn't has not been confirmed yet!" };

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (!result.Succeeded)
                return new BaseResponse { Message = result.Errors.Select(x => x.Description) };


            return new BaseResponse { Succeeded = true, Message = "The account password has been updated successfully" };
        }

        public async Task<BaseResponse> ConfirmEmailAsync(string userId, string code)
        {
//var token = code = HttpUtility.UrlDecode(code); ;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new BaseResponse { Message = "The account does not exist" };

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
                return new BaseResponse { Message = result.Errors.Select(x => x.Description) };

            return new BaseResponse { Succeeded = true, Message = "The email account has been verified" };

        }

        public async Task<BaseResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
                return new BaseResponse { Message = "The account doesn't exist" };

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callBackUrl = $"{Links.ClientBaseUrl}/reset-password?userId={user.Id}&code={token}";

            var body = await File.ReadAllTextAsync("Assests/Templates/reset-password.html");
            body = body.Replace("##Name", user.FirstName);
            body = body.Replace("##CallBackUrl", callBackUrl);

            await _emailService.SendMailAsync(user.Email, "Reset Password", body);

            return new BaseResponse { Succeeded=true, Message = "Password reset link has been sent to your email" };
        }

        public async Task<BaseResponse> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new BaseResponse { Message = "The login attempt failed" };

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new BaseResponse { Message = "The login attempt failed" };

            var result = await _userManager.CheckPasswordAsync(user, password);

            if (!result)
                return new BaseResponse { Message = "The login attempt failed" };

            return new BaseResponse { Succeeded = true, Message = "Login successful", Data = CreateUserToken(user) };
        }

        public async Task<BaseResponse> RegisterAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return new BaseResponse { Message = result.Errors.Select(x => x.Description) };

            await SendEmailConfirmEmail(user);

            return new BaseResponse { Succeeded = true, Message = "Confirmation link has been sent to your mail", Data = CreateUserToken(user) };
        }

        public async Task<BaseResponse> ResendConfirmationEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new BaseResponse { Message = "The account doesn't exist" };

            await SendEmailConfirmEmail(user);

            return new BaseResponse { Succeeded = true, Message = "Confirmation link has been sent to your mail"};
        }

        public async Task<BaseResponse> ResetPasswordAsync(string userId, string code, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new BaseResponse { Message = "The user account doesn't exists!" };

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new BaseResponse { Message = "The user account is not yet verified!" };

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var result = await _userManager.ResetPasswordAsync(user, token, password);

            if (!result.Succeeded)
                return new BaseResponse { Message = result.Errors.Select(x => x.Description) };

            return new BaseResponse { Succeeded = true, Message = "The user password has been updated" };
        }

        private string CreateUserToken(ApplicationUser user)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSetting.Value.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Email,user.Email)
                }),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private async Task SendEmailConfirmEmail(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = HttpUtility.UrlEncode(code);

            var callBackUrl = $"{Links.ClientBaseUrl}/confirm?userId={user.Id}&code={code}";

            var body = await File.ReadAllTextAsync("Assests/Templates/confirm-email.html");

            body = body.Replace("##Name", user.FirstName);
            body = body.Replace("##callBackUrl", callBackUrl);

            await _emailService.SendMailAsync(user.Email, "Confirm Email", body);
        }
    }
}
