using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GymMembershipManagementSystem.Models
{


    public class EndDateGreaterThanStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (MembershipRegistration)validationContext.ObjectInstance;
            var startDate = model.StartDate;
            var endDate = (DateTime)value;

            if (endDate <= startDate)
            {
                return new ValidationResult("End date must be greater than the start date.");
            }

            return ValidationResult.Success;
        }
    }

}