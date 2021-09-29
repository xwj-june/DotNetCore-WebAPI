﻿using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Filters.V2;

namespace WebApi.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/tickets")]
    public class TicketsV2Controller : ControllerBase
    {
		private readonly BugsContext db;

		public TicketsV2Controller(BugsContext db)
		{
            this.db = db;
		}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await db.Tickets.ToListAsync()); //return http code with content
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(await db.Tickets.FindAsync(id));
        }


        [HttpPost]
        [Ticket_EnsureDescriptionPresentActionFilterAttribute]
        public async Task<IActionResult> Post([FromBody] Ticket ticket)
        {
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = ticket.ProjectId },
                ticket);

        }

        [HttpPut("{id}")]
        [Ticket_EnsureDescriptionPresentActionFilterAttribute]
        public async Task<IActionResult> Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId)
                return BadRequest();

            db.Entry(ticket).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                if (await db.Tickets.FindAsync(id) == null)
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync();

            return Ok(ticket);
        }

    }
}
