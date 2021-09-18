using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Controllers
{
    //3. inherits ContorllerBase and decorate with ApiController
    [ApiController]
    [Route("api/[controller]")] //attribute routing in Contorller
    public class TicketsController : ControllerBase //ControllerBase contains everything needs for Web API controller
    {
        [HttpGet]
        //[Route("api/tickets")] //Attribute routing in Method
        public IActionResult Get() //IActionResult is a general type can hold different type of output like json
        {
            return Ok("Reading all the tickets."); //return http code with content
        }


        [HttpGet("{id}")]
        //[HttpGet]
        //[Route("api/tickets/{id}")] //Attribute routing
        public IActionResult GetById(int id)
        {
            return Ok($"Reading ticket #{id}.");
        }


        [HttpPost]
        //[Route("api/tickets")] //Attribute routing
        public IActionResult Post()
        {
            return Ok("Creating a ticket.");
        }

        [HttpPut]
        //[Route("api/tickets")] //Attribute routing
        public IActionResult Put()
        {
            return Ok("Updating a ticket");
        }

        [HttpDelete("{id}")]
        //[HttpDelete]
        //[Route("api/tickets/{id}")] //Attribute routing
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting ticket #{id}.");
        }

    }
}
