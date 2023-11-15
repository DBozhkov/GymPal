namespace GymPalApi.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class UserRolesResponseViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<RoleUserViewModel> Roles { get; set; }
    }
}
