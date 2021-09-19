using Microsoft.AspNetCore.Mvc;
using PlatformDemo.Models;
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
        public IActionResult Post([FromBody] Ticket ticket)
        {
            return Ok(ticket); // it will automaticlly serilize the object to json
        }

        //test above in PowerShell
        // $body =@{
        // ProjectId=1
        // Title = "This is a title"
        // Description = "this is a desc"
        // }
        //$jsonBody = ConvertTo-Json -InputObject $body
        //$Response = Invoke-RestMethod -Uri 'https://localhost:44314/api/tickets' -Method Post -ContentType 'application/json' -Body $jsonBody
        //$Response


        [HttpPut]
        //[Route("api/tickets")] //Attribute routing
        public IActionResult Put([FromBody] Ticket ticket)
        {
            return Ok(ticket);
        }


        //test above in PowerShell
        // $body =@{
        // TicketId=100
        // ProjectId=1
        // Title = "This is a title"
        // Description = "this is a desc"
        // }
        //$jsonBody = ConvertTo-Json -InputObject $body
        //$Response = Invoke-RestMethod -Uri 'https://localhost:44314/api/tickets' -Method Put -ContentType 'application/json' -Body $jsonBody
        //$Response

        [HttpDelete("{id}")]
        //[HttpDelete]
        //[Route("api/tickets/{id}")] //Attribute routing
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting ticket #{id}.");
        }

    }
}
