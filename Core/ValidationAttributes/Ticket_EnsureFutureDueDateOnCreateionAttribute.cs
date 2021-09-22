﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace Core.ValidationAttributes
{
	public class Ticket_EnsureFutureDueDateOnCreateionAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var ticket = validationContext.ObjectInstance as Ticket;
			if (!ticket.ValidateFutureDueDate())
				return new ValidationResult("Due date has to be in the future.");
			return ValidationResult.Success;
		}

	}
}
