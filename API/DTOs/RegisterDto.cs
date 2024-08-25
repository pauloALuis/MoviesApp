using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(15, MinimumLength = 2)]
        public required string Username { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 2)]
        public required string Password { get; set; }

    }
}