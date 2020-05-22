using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRental.ViewModels
{
    public class MovieFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required()]
        [Display(Name = "Release Date")]
        public DateTime? DateReleased { get; set; }

        [Required]
        [Display(Name = "Number in Stock")]
        [Range(1, 25)]
        public short? NumberInStock { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public short? GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public MovieFormViewModel()
        {

        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            DateReleased = movie.DateReleased;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}