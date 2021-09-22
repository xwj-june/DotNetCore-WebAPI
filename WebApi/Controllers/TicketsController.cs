﻿using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase 
    {
		private readonly BugsContext db;

		public TicketsController(BugsContext db)
		{
            this.db = db;
		}

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Tickets.ToList()); //return http code with content
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ticket = db.Tickets.Find(id);
            if (ticket == null) 
                return NotFound();

            return Ok(db.Tickets.Find(id));
        }


        [HttpPost]
        public IActionResult PostV1([FromBody] Ticket ticket)
        {
            db.Tickets.Add(ticket);
            db.SaveChanges();

            return CreatedAtAction(nameof(GetById),
                new { id = ticket.ProjectId },
                ticket);
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) 
                return BadRequest();

            db.Entry(ticket).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch
            {
                if (db.Tickets.Find(id) == null)
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = db.Tickets.Find(id);
            if (ticket == null) return NotFound();

            db.Tickets.Remove(ticket);
            db.SaveChanges();

            return Ok(ticket);
        }

    }
}
