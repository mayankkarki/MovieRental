using MovieRental.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

     
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        //Loading MembershiType table together with Customer table is called Eager loading
        public MembershipType MembershipType { get; set; }
              
        [Required]      
        public byte MembershipTypeId { get; set; }
    }
}