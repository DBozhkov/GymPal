using GymPalApi.Data.Enums;
using GymPalApi.Data.Services;
using GymPalApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymPalApi.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost, Route(nameof(GetById)), Authorize]
        public async Task<ActionResult<UserRolesResponseViewModel>> GetById([FromBody] string id)
        {
            try
            {
                var user = await this.userService.GetUserWithRolesAsync(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route(nameof(AddToRole)), Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserRolesResponseViewModel>> AddToRole(AddUserToRole model)
        {
            try
            {
                var user = await this.userService.AddToRoleAsync(model);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
