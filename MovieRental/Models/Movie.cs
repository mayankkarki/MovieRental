using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace MovieRental.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime DateReleased { get; set; }

        [Required]
        [Display(Name = "Number in Stock")]
        public short NumberInStock { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        public short GenreId { get; set; }

    }
}