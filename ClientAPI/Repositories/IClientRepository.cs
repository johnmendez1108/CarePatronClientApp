using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientAPI.Model;
namespace ClientAPI.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientById(int id);
        void AddClient(Client client);
        void UpdateClient(Client client);
        IEnumerable<Client> SearchClients(string searchTerm);
    }
}
