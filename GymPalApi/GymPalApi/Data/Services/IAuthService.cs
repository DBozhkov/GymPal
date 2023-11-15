namespace GymPalApi.Data.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GymPalApi.ViewModels;

    public interface IAuthService
    {
        Task<string> GenerateJwtToken(string userId, ICollection<string> userRoles);

        Task SetRefreshTokenAsync(RefreshToken refreshToken, string userId);

        Task DeleteRefreshTokenAsync(string token);

        RefreshToken CreateRefreshToken();

        Task<AuthUserViewModel> FindUserByRefreshTokenAsync(string refreshToken);
    }
}
