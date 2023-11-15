using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GymPalApi.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? TokenCreated { get; set; }

        public DateTime? TokenExpires { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
