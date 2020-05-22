using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public byte DurationInMonths { get; set; }

        public short SignUpfee { get; set; }

        public byte DiscountRate { get; set; }

    }
}