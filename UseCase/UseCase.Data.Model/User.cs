using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace UseCase.Data.Model
{
    public  abstract class User : IdentityUser<Guid>
    {

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public bool IsActive { get; set; }
       
    }
}