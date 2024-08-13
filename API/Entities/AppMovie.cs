using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppMovie
    {

        [Key]
        [Display(Name = "Nº User")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title Movie")]
        public required string Title { get; set; }

        [Display(Name = "Written by")]
        public string WrittenBy { get; set; }

        [Display(Name = "Production Company")]
        public required String ProductionBy { get; set; }

        [Display(Name = "Genres/Category")]
        public required String Genres { get; set; }

        [Required]
        [Display(Name = "Running time")]
        public required TimeSpan RunningTime { get; set; }

    }
}