using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace UseCase.Data.Model
{
    public abstract class Role : IdentityRole<Guid>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}