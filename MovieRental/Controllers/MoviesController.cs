using MovieRental.Models;
using MovieRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Controllers
{
    public class MoviesController : Controller
    {
        private MovieRentalDbContext _context;

        public MoviesController()
        {
            _context = new MovieRentalDbContext();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "3 Idiots!" };

            var customers = new List<Customer>()
            {
            new Customer{ Name = "Customer 1"},
            new Customer{ Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);

            //var viewResult = new ViewResult();
            //viewdata dictionary has model property which act as source of key value pairs
            //viewResult.ViewData.Model = movie;
            //return viewResult;

            //return Content("Hello world");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "Name" });
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
            var viewModel = new MovieViewModel
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
            var viewModel = new MovieViewModel
            {
                Movie = movie,
                Genres = genres
            };

            return View("AddEditForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
                _context.Movies.Add(movie);
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