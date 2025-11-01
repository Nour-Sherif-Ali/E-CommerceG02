﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityModule
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        #region Relationship with Address Module (Navigation Property)
        public Address? Address { get; set; }
        #endregion
    }
}
