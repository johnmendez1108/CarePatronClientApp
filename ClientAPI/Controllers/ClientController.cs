using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClientAPI.Repositories;
using ClientAPI.Services;
using ClientAPI.Model;

namespace ClientAPI.Controllers
{
   
    [Route("v1/api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly MailService _emailService;
        private readonly DocumentSyncService _documentSyncService;

        public ClientController(
            IClientRepository clientRepository,
            MailService emailService,
            DocumentSyncService documentSyncService)
        {
            _clientRepository = clientRepository;
            _emailService = emailService;
            _documentSyncService = documentSyncService;
        }

        [HttpPost]
        public IActionResult CreateClient(Client client)
        {
            _clientRepository.AddClient(client);

            _emailService.SendEmail(client);
            _documentSyncService.SyncDocuments(client);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, Client client)
        {
            var existingClient = _clientRepository.GetClientById(id);
            if (existingClient == null)
                return NotFound();
            client.Id = id;
            _clientRepository.UpdateClient(client);

            if (client.Email != existingClient.Email)
            {
                _emailService.SendEmail(client);
                _documentSyncService.SyncDocuments(client);
            }

            return Ok();
        }

        [HttpGet("search")]
        public IActionResult SearchClients(string searchTerm)
       {
            if (searchTerm == null)
                searchTerm = string.Empty;

            var searchResults = _clientRepository.SearchClients(searchTerm);
            return Ok(searchResults);
        }
    }
}
