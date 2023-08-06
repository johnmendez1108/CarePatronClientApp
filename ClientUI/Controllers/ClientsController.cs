using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ClientUI.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetClients()
        {
            // Replace this with your actual data retrieval logic
            var clients = new List<Client>
        {
            new Client { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" },
            new Client { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" }
        };

            return Ok(clients);
        }
    }
}
