using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRental.ViewModels
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
    }
}