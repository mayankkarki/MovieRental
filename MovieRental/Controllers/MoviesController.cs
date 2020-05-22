using MovieRental.Models;
using MovieRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace MovieRental.Controllers
{
    public class MoviesController : Controller
    {
        private MovieRentalDbContext _context;

        public MoviesController()
        {
            _context = new MovieRentalDbContext();
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(nameof(Movie.Genre)).SingleOrDefault(obj => obj.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;

        //    if (string.IsNullOrWhiteSpace(sortBy))
        //        sortBy = nameof(Movie.Name);

        //    //return Content($"pageIndex={pageIndex}&sortBy={sortBy}");
        //    var movies = GetMovies();
        //    return View(movies);
        //}

        public ActionResult Index()
        {
            var movies = _context.Movies.Include(nameof(Movie.Genre)).ToList();
            return View(movies);
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres              
            };

            return View("AddEditForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel(movie)
            {               
                Genres = genres
            };

            return View("AddEditForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var genres = _context.Genres.ToList();
                var viewModel = new MovieFormViewModel(movie)
                {                 
                    Genres = genres
                };

                return View("AddEditForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.DateReleased = movie.DateReleased;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.Name = movie.Name;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}