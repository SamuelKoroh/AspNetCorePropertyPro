using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Response;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IAuthService
    {
        Task<BaseResponse> RegisterAsync(ApplicationUser user, string password);
        Task<BaseResponse> ConfirmEmailAsync(string userId, string code);
        Task<BaseResponse> LoginAsync(string email, string password);
        Task<BaseResponse> ForgetPasswordAsync(string email);
        Task<BaseResponse> ResetPasswordAsync( string userId, string code, string password);
        Task<BaseResponse> ChangePasswordAsync( string userId, string oldPassword, string newPassword);
    }
}
