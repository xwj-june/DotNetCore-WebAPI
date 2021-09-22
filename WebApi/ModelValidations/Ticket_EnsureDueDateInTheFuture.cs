using PlatformDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.ModelValidations
{
    public class Ticket_EnsureDueDateInTheFuture : ValidationAttribute //custom validation
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //this validation should only action when Post(Create) ticket
            var ticket = validationContext.ObjectInstance as Ticket;
            if (ticket != null && ticket.TicketId == null)
            {
                if (ticket.DueDate.HasValue && ticket.DueDate.Value < DateTime.Now) //any nullable object has this HasValue method
                    return new ValidationResult("Due date has to be in the future");
            }
            return ValidationResult.Success;

        }

    }
}
