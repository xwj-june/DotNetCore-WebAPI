﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Models
{
    public class Ticket
    {
        public int? TicketId { get; set; }
        [Required]
        public int? ProjectId { get; set; } //int? -> make it's default value to be null, otherwise will be 0
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
