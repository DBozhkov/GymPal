using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GymPalApi;
using GymPalApi.Data;
using GymPalApi.Data.Models;
using GymPalApi.Data.Services;
using GymPalApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly ApplicationSettings _appSettings;
    private readonly IUserService usersService;
    private readonly ApplicationDbContext context;

    public AuthService(
                         IOptions<ApplicationSettings> appSettings,
                         IUserService usersService,
                         ApplicationDbContext context
                      )
    {
        this._appSettings = appSettings.Value;
        this.usersService = usersService;
        this.context = context;
    }

    public async Task<string> GenerateJwtToken(string userId, ICollection<string> userRoles)
    {
        var user = await this.usersService.GetUserAsync(userId);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(this._appSettings.JwtSecret);

        var claimsList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.DateOfBirth, user.CreatedOn.ToString()),
        };

        foreach (var role in userRoles)
        {
            claimsList.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claimsList),
            Expires = DateTime.UtcNow.AddHours(1), 
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


    public async Task SetRefreshTokenAsync(RefreshToken refreshToken, string userId)
    {
        var user = await this.usersService.GetUserAsync(userId);

        user.RefreshToken = refreshToken.Token;
        user.TokenCreated = refreshToken.CreatedOn;
        user.TokenExpires = refreshToken.Expires;

        await context.SaveChangesAsync();
    }

    public RefreshToken CreateRefreshToken()
    {
        byte[] randomNumber = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(randomNumber),
            Expires = DateTime.UtcNow.AddDays(1),
            CreatedOn = DateTime.UtcNow,
        };

        return refreshToken;
    }

    public async Task DeleteRefreshTokenAsync(string token)
    {
        var user = await this.context.Users.FirstOrDefaultAsync(x => x.RefreshToken == token && x.IsDeleted != true);
        user.RefreshToken = "";
        await this.context.SaveChangesAsync();
    }

    public async Task<AuthUserViewModel> FindUserByRefreshTokenAsync(string token)
    {
        var user = await this.context.Users
          .Where(x => x.RefreshToken == token && x.IsDeleted != true)
          .Select(x => new AuthUserViewModel
          {
              Id = x.Id,
              Email = x.Email,
              Username = x.UserName,
              RefreshToken = x.RefreshToken,
              TokenCreated = (DateTime)x.TokenCreated,
              TokenExpires = (DateTime)x.TokenExpires,
          })
          .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found!");
        }

        var roles = await this.usersService.GetUserRolesNamesAsync(user.Id);
        user.Roles = roles;

        return user;
    }
   
}