using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppUser
    {
        private const string messageError = "Field is required";

        [Key]
        //[Display(Name = "Nº User")]
        public int Id { get; set; }
        [Required(ErrorMessage = messageError)]
        //[Display(Name = "UserName", Description = "Name for your user")]
        [StringLength(15, MinimumLength = 2)]
        public string UserName { get; set; }
        //[Display(Name = "Password")]
        [Required(ErrorMessage = messageError)]
        public required byte[] PasswordHash { get; set; }
        
        [Required(ErrorMessage = messageError)]
        public required byte[] PasswordSalt { get; set; }
    }
}