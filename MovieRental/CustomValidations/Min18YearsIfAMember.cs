using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MovieRental.CustomValidations
{
    /// <summary>
    /// Validator for a businees rule, If membership type other than Pay as you go is selected
    /// Then customer should be at least 18 years old.
    /// </summary>
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            //in case member type is not selected or Pay as you go is selected
            //then return as valid result
            if (customer.MembershipTypeId == MembershipType.PayAsYouGoId
                || customer.MembershipTypeId == MembershipType.UnknownId)
                return ValidationResult.Success;

            if (!customer.BirthDate.HasValue)
                return new ValidationResult("BirthDate is required");

            var age = DateTime.Now.Year - customer.BirthDate.Value.Year;
            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go on a membership");
        }
    }
}