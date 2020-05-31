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
    public class RentalsController : ApiController
    {
        private MovieRentalDbContext _dbContext;

        public RentalsController()
        {
            _dbContext = new MovieRentalDbContext();
        }

        [Authorize(Roles = Constants.RoleNames.CanManageMovies)]
        //api/rental
        [HttpPost]
        public IHttpActionResult CreateRental(RentalDto rentalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _dbContext.Customers.Single(c => c.Id == rentalDto.CustomerId);
            var movies = _dbContext.Movies.Where(m => rentalDto.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    CustomerId = rentalDto.CustomerId,
                    DateRented = DateTime.Now,
                    MovieId = movie.Id
                };

                _dbContext.Rentals.Add(rental);
            }

            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
