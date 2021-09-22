using PlatformDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.ModelValidations
{
    public class Ticket_EnsureDueDateForTicketOwner : ValidationAttribute //custom validation
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //this objectInstance is refer to the object that use this validation
            var ticket = validationContext.ObjectInstance as Ticket;
            if (ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                if (!ticket.DueDate.HasValue) //any nullable object has this HasValue method
                    return new ValidationResult("Due date is requried when the ticket has an owner");
            }
            return ValidationResult.Success;

        }

    }
}
