using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRental.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required()]
        public DateTime DateReleased { get; set; }

        [Required]
        [Range(1, 25)]
        public short NumberInStock { get; set; }

        [Required]
        public short GenreId { get; set; }

        public GenreDto Genre { get; set; }
    }
}