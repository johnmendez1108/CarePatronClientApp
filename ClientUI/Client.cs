using System;
using System.Text.Json.Serialization;
namespace ClientUI
{
    public class Client
    {        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
