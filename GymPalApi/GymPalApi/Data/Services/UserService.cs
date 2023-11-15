using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymPalApi.Data;
using GymPalApi.Data.Enums;
using GymPalApi.Data.Models;
using GymPalApi.Data.Services;
using GymPalApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ShopHeaven.Data.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(
            UserManager<User> userManager,
            ApplicationDbContext db,
            IHttpContextAccessor httpContextAccessor,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
            this.roleManager = roleManager;
        }

        public async Task RegisterAsync(CreateUserInputModel model)
        {
            User dbUser = await db.Users.FirstOrDefaultAsync(x => x.Email == model.Email.Trim() && x.IsDeleted != true);

            if (dbUser != null)
            {
                throw new ArgumentException("This email already exists!");
            }

            if (model.Username.Length < 3 || model.Username.Length > 30)
            {
                throw new ArgumentException("Username should be between 3 and 30 symbols!");
            }


            if (model.Password.Trim() != model.ConfirmPassword.Trim())
            {
                throw new ArgumentException("Your passwords don't match!");
            }

            if (model.Password.Length < 3 || model.Password.Length > 20)
            {
                throw new ArgumentException("Password is not between 3 and 20 characters!");
            }

            if (!(model.Password.Any(char.IsDigit) && !model.Password.Any(char.IsLetter)))
            {
                throw new ArgumentException("Password must contain letters and digits");
            }

            User user = new User
            {
                UserName = model.Username,
                Email = model.Email.Trim(),
                IsDeleted = false,
                TokenCreated = null,
                TokenExpires = null,
                RefreshToken = ""
            };


            var result = await userManager.CreateAsync(user, model.Password.Trim());

            if (!result.Succeeded)
            {
                throw new ArgumentException("User is not created");
            }

            var adminRole = Roles.Admin.ToString();

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            await userManager.AddToRoleAsync(user, adminRole);
        }

        public async Task<UserViewModel> GetAuthInfoByUsername(string username)
        {
            var user = await this.db.Users
                .Where(x => x.UserName == username && x.IsDeleted != true)
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    Username = x.UserName,
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var userRoles = await GetUserRolesNamesAsync(user.Id);

            user.Roles = userRoles;

            return user;
        }

        public async Task<IList<string>> GetUserRolesNamesAsync(string userId)
        {
            var user = await this.GetUserAsync(userId);

            IList<string> userRoles = await userManager.GetRolesAsync(user);

            return userRoles;
        }

        public async Task<bool> ValidatePasswordAsync(string userId, string password)
        {
            User user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("User Does Not Exist");
            }

            var passwordValid = await userManager.CheckPasswordAsync(user, password);

            return passwordValid;
        }

        public async Task<UserRolesResponseViewModel> AddToRoleAsync(AddUserToRole model)
        {
            var isUserInThisRole = await this.db.UserRoles
                .AnyAsync(x => x.RoleId == model.RoleId && x.UserId == model.UserId);

            if (isUserInThisRole)
            {
                throw new ArgumentException("The user already has this role");
            }

            var user = await this.GetUserAsync(model.UserId);

            if (user == null)
            {
                throw new ArgumentNullException("No User");
            }

            var role = await this.db.Roles
                .FirstOrDefaultAsync(x => x.Id == model.RoleId);

            if (role == null)
            {
                throw new ArgumentNullException("Role does not exist");
            }

            var userRole = new IdentityUserRole<string>()
            {
                RoleId = model.RoleId,
                UserId = model.UserId,
            };

            await this.db.UserRoles.AddAsync(userRole);

            await this.db.SaveChangesAsync();

            var userModel = await GetUserWithRolesAsync(model.UserId);

            return userModel;

        }

        public async Task<User> GetUserAsync(string userId)
        {
            var user = await this.db.Users
                .FirstOrDefaultAsync(x => x.Id == userId && x.IsDeleted != true);

            if (user == null)
            {
                throw new ArgumentException("This user does not exist");
            }

            return user;
        }

        public async Task<UserRolesResponseViewModel> GetUserWithRolesAsync(string userId)
        {
            var user = await this.db.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserRolesResponseViewModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    CreatedOn = u.CreatedOn,
                    IsDeleted = u.IsDeleted,
                    Roles = this.db.UserRoles
                        .Where(ur => ur.UserId == u.Id)
                        .Select(ur => new RoleUserViewModel
                        {
                            RoleId = ur.RoleId,
                            Name = this.db.Roles.FirstOrDefault(r => r.Id == ur.RoleId).Name,
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return user;
        }

    }
}