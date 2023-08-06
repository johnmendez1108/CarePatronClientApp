using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClientAPI.Model;

namespace ClientAPI.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private List<Client> _clients = new List<Client>();
        private int _nextClientId = 1;

        private const string FilePath ="client_data.txt";

        public ClientRepository()
        {
            LoadDataFromFile();
        }

        public void AddClient(Client client)
        {
            client.Id = _nextClientId++;
            _clients.Add(client);
            SaveDataToFile();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clients;
        }

        public Client GetClientById(int id)
        {
            return _clients.FirstOrDefault(client => client.Id == id);
        }

        public IEnumerable<Client> SearchClients(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            return _clients
                .Where(client =>
                    client.FirstName.ToLower().Contains(searchTerm) ||
                    client.LastName.ToLower().Contains(searchTerm))
                .ToList();
        }

        public void UpdateClient(Client client)
        {
            var existingClient = _clients.Find(c => c.Id == client.Id);
            if (existingClient != null)
            {
                existingClient.FirstName = client.FirstName;
                existingClient.LastName = client.LastName;
                existingClient.Email = client.Email;
                SaveDataToFile();
            }
        }


        private void LoadDataFromFile()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + FilePath))
            {
                var lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + FilePath);
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    if (data.Length == 4 &&
                        int.TryParse(data[0], out int id) &&
                        !string.IsNullOrWhiteSpace(data[1]) &&
                        !string.IsNullOrWhiteSpace(data[2]) &&
                        !string.IsNullOrWhiteSpace(data[3]))
                    {
                        _clients.Add(new Client
                        {
                            Id = id,
                            FirstName = data[1],
                            LastName = data[2],
                            Email = data[3]
                        });
                    }
                }
            }
        }

        private void SaveDataToFile()
        {
            var lines = _clients.Select(client =>
                $"{client.Id},{client.FirstName},{client.LastName},{client.Email}");
            File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + FilePath, lines);
        }
    }
}
