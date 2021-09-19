using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlatformDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Filters
{
    public class Ticket_ValidateDatesActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var ticket = context.ActionArguments["ticket"] as Ticket;

            if (ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                bool isValid = true;

                if (ticket.EnteredDate.HasValue == false)
                {
                    context.ModelState.AddModelError("EnteredDate", "EnteredDate is required");
                    isValid = false;
                }

                if (ticket.EnteredDate.HasValue && ticket.DueDate.HasValue && ticket.EnteredDate > ticket.DueDate)
                {
                    context.ModelState.AddModelError("DueDate", "DueDate must be later than EnteredDate ");
                    isValid = false;
                }

                if (isValid == false)
                {
                    context.Result = new BadRequestObjectResult(context.ModelState);
                }
            }
        }
    }
}
