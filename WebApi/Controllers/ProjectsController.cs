using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
		private readonly BugsContext db;

		public ProjectsController(BugsContext db)
		{
            this.db = db;
		}
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Projects.ToList());
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id) //id from route
        {
            var project = db.Projects.Find(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        /// <summary>
        /// api/projects/{pid}/tickets?tid={tid}
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        //api/project/45/tickets
        //api/project/45/tickets?tid=123
        public IActionResult GetProjectTIcket(int pId, [FromQuery] int tId)
        {
            var tickets = db.Tickets.Where(t => t.ProjectId == pId).ToList();
            if (tickets == null || tickets.Count() <= 0)
            {
                return NotFound();
            }
            return Ok(tickets);
        }

        #region no common - model binding for complex type from both Route and Query
        //[HttpGet]
        //[Route("/api/projects/{pid}/tickets")]
        ////api/projects/45/tickets
        ////api/projects/45/tickets?tid=123
        //public IActionResult GetProjectTicket1([FromQuery]Ticket ticket)
        //    //by defult, a complex type will look for values from HTTP request body
        //    //[FromQuery] makes ticket has to come from Query, however, pid will still come from the route becuase the Ticket class will override it for pid
        //{
        //    if (ticket == null)
        //    {
        //        return BadRequest("Parameters are not provided propertly");
        //    }
        //    if (ticket.TicketId == 0)
        //    {
        //        return Ok($"Reading all tickets belong to project {ticket.ProjectId}");
        //    }
        //    return Ok($"Reading project {ticket.ProjectId}, ticket #{ticket.TicketId},title: {ticket.Title}, description: {ticket.Description}");
        //}
        #endregion

        [HttpPost]
        public IActionResult Post([FromBody]Project project)
        {
            db.Projects.Add(project); //change tracker will mark this project as "Added". there are different states - added,modified,deleted etc.
            db.SaveChanges();

            return CreatedAtAction(nameof(GetById), 
                new { id = project.ProjectId }, 
                project);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Project project)
        {
            if (id != project.ProjectId) return BadRequest();

            db.Entry(project).State = EntityState.Modified;
			try
			{
                db.SaveChanges();
            }
			catch
			{
                if (db.Projects.Find(id) == null)
                    return NotFound();
				throw;
			}

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = db.Projects.Find(id);
            if (project == null) return NotFound();

            db.Projects.Remove(project);
            db.SaveChanges();

            return Ok(project);
        }

    }
}
