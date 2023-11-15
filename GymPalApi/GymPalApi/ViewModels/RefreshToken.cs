using System;

namespace GymPalApi.ViewModels
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime Expires { get; set; }
    }
}