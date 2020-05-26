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

        [Required()]
        public DateTime DateReleased { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public short GenreId { get; set; }

        [Required]
        [Range(1, 25)]
        public short NumberInStock { get; set; }

        [Required]
        [Range(1, 25)]
        public short NumberAvailable { get; set; }
    }
}