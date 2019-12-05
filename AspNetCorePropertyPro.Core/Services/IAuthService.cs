using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IAuthService
    {
        Task<ApplicationUser> RegisterAsync(ApplicationUser user);
        Task<ApplicationUser> LoginAsync(string email, string password);
    }
}
