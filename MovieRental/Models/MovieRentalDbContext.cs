using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    public class MovieRentalDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<MembershipType> MembershipTypes { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public static MovieRentalDbContext Create()
        {
            return new MovieRentalDbContext();
        }
    }
}