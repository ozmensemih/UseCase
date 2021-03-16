﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase.Data.Model
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
