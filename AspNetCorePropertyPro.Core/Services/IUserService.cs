using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> UpdateUser(ApplicationUser userToUpdate, ApplicationUser user);
        Task<ApplicationUser> GetUserByID(string userId);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task DeleteUser(ApplicationUser user);
    }
}
