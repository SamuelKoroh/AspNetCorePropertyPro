using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorePropertyPro.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task DeleteUser(ApplicationUser user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByID(string userId)
        {
            return await _userManager.Users
                .Include(p => p.Properties)
                .SingleOrDefaultAsync(x => x.Id == userId);
        }
        
        public async Task<ApplicationUser> UpdateUser(ApplicationUser userToUpdate, ApplicationUser user)
        {
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Address = user.Address;
            userToUpdate.PhoneNumber = user.PhoneNumber;

            await _userManager.UpdateAsync(userToUpdate);

            return userToUpdate;
        }
    }
}
