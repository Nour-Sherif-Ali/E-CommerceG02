﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.IdentityDto
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
