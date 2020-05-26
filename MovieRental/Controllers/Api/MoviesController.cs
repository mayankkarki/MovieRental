using AutoMapper;
using MovieRental.Dtos;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MovieRental.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private Mapper _mapperInstance;
        private MovieRentalDbContext _context;

        public MoviesController()
        {
            _mapperInstance = new Mapper(MvcApplication.AutoMapperConfiguation);
            _context = new MovieRentalDbContext();
        }

        //Get api/movies
        public IHttpActionResult Getmovies()
        {
            var movies = _context.Movies
                .Include(nameof(Movie.Genre))
                .ToList()
                .Select(_mapperInstance.Map<Movie, MovieDto>);
            return Ok(movies);
        }

        //Get api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(_mapperInstance.Map<Movie, MovieDto>(movie));
        }

        [Authorize(Roles = Constants.RoleNames.CanManageMovies)]
        //Post api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _mapperInstance.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [Authorize(Roles = Constants.RoleNames.CanManageMovies)]
        //Put api/movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            _mapperInstance.Map(movieDto, movie);
            _context.SaveChanges();
            return Ok();
        }

        [Authorize(Roles = Constants.RoleNames.CanManageMovies)]
        //Delete api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }
    }
}
