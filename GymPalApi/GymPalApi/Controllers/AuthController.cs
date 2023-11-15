using GymPalApi.Data.Services;
using GymPalApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymPalApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            this.userService = userService;
            this.authService = authService;
        }

        [HttpPost, Route(nameof(Register))]
        public async Task<ActionResult> Register(CreateUserInputModel model)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            try
            {
                await this.userService.RegisterAsync(model);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This email already exists!")
                {
                    return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
                }

                return BadRequest(ex.Message);
            }

            return Ok("Register successful");
        }


        [HttpPost, Route(nameof(Login))]
        public async Task<ActionResult<AuthUserViewModel>> Login(LoginUserInputModel model)
        {
            try
            {
                var user = await this.userService.GetAuthInfoByUsername(model.Username.Trim());

                if (user == null)
                {
                    return Unauthorized("User is not found");
                }

                var passwordValid = await this.userService.ValidatePasswordAsync(user.Id, model.Password);

                if (!passwordValid)
                {
                    return Unauthorized("Password is not valid!");
                }

                IList<string> userRoles = await this.userService.GetUserRolesNamesAsync(user.Id);

                string jwtToken = await this.authService.GenerateJwtToken(user.Id, userRoles);

                RefreshToken refreshToken = this.authService.CreateRefreshToken();
                await SetRefreshToken(refreshToken, user.Id);

                var response = new AuthUserViewModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Roles = user.Roles,
                    JwtToken = jwtToken,
                    RefreshToken = refreshToken.Token,
                    TokenCreated = refreshToken.CreatedOn,
                    TokenExpires = refreshToken.Expires,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet(nameof(RefreshToken))]
        public async Task<ActionResult<AuthUserViewModel>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var user = new AuthUserViewModel();

            try
            {
                user = await this.authService.FindUserByRefreshTokenAsync(refreshToken);

                if (!user.RefreshToken.Equals(refreshToken))
                {
                    return Unauthorized("Invalid Refresh Token.");
                }
                else if (user.TokenExpires < DateTime.UtcNow)
                {
                    return Unauthorized("Token expired.");
                }

                //var roles = await this.usersService.GetUserRolesNamesAsync(userModel.Id);

                string token = await this.authService.GenerateJwtToken(user.Id, user.Roles);


                var newRefreshToken = this.authService.CreateRefreshToken();
                await SetRefreshToken(newRefreshToken, user.Id);

                user.JwtToken = token;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


            return Ok(user);
        }


        [HttpPost, Route(nameof(Logout))]
        public async Task<ActionResult> Logout([FromBody] LogoutRequest request)
        {
            var refreshToken = request.RefreshToken;

            try
            {
                var user = await this.authService.FindUserByRefreshTokenAsync(refreshToken);

                if (user == null)
                {
                    return BadRequest("User does not exist!");
                }

                await this.authService.DeleteRefreshTokenAsync(refreshToken);

                Response.Cookies.Delete("refreshToken");

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task SetRefreshToken(RefreshToken newRefreshToken, string userId)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = newRefreshToken.Expires,
            };

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            await this.authService.SetRefreshTokenAsync(newRefreshToken, userId);
        }
    }
}
