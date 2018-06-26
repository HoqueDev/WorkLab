using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    class AuthRequest
    {
        [JsonProperty("email")] //Gets email from API 
        public string Email { get; set; }

        [JsonProperty("password")] //Gets Password from API
        public string Password { get; set; }

        public AuthRequest(string email, string password)
        {
            //populates the applications memory with the values returned from the API (which comes from the database)
            this.Email = email;
            this.Password = password;
        }
    }
}
