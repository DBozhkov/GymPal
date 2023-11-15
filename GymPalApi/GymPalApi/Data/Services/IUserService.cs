
using GymPalApi.Data.Models;
using GymPalApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymPalApi.Data.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string userId);

        Task RegisterAsync(CreateUserInputModel model);

        Task<UserRolesResponseViewModel> GetUserWithRolesAsync(string userId);

        Task<bool> ValidatePasswordAsync(string userId, string password);

        Task<IList<string>> GetUserRolesNamesAsync(string userId);

        Task<UserViewModel> GetAuthInfoByUsername(string username);

        Task<UserRolesResponseViewModel> AddToRoleAsync(AddUserToRole model);
    }
}
