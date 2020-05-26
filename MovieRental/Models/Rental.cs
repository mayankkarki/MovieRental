using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }
    }
}