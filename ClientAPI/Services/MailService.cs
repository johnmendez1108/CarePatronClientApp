using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientAPI.Model;

namespace ClientAPI.Services
{
    public class MailService
    {
        public void SendEmail(Client client)
        {
            // Implement email sending logic here
            Console.WriteLine($"Sending email to {client.Email}: Client {client.FirstName} {client.LastName} has been created/updated.");
        }
    }
}
