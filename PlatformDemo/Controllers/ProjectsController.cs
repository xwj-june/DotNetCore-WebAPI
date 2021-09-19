using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Reading all the projects.");
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id) //id from route
        {
            return Ok($"Reading project #{id}.");
        }

        /// <summary>
        /// api/projects/{pid}/tickets?tid={tid}
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        //api/project/45/tickets
        //api/project/45/tickets?tid=123
        public IActionResult GetProjectTIcket(int pId, [FromQuery] int tId) //[FromQuery] makes tId has to come from Query
        {
            if (tId == 0)
            {
                return Ok($"Reading all tickets belong to project {pId}");
            }
            return Ok($"Reading project {pId}, ticket #{tId}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Creating a project.");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating a project");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting project #{id}.");
        }

    }
}
