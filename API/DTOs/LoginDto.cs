using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class LoginDto
    {
        [StringLength(10, MinimumLength = 2)]
        public required string Username { get; set; }
        [StringLength(18, MinimumLength = 2)]
        public required string Password { get; set; }
    }
}