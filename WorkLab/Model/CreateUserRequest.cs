using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    class CreateUserRequest
    {
        //Each JsonProperty creates a Key which the API can use to recognise what piece of data it is.
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("is_teacher")]
        public bool IsTeacher { get; set; }

        public CreateUserRequest(string name, string email, string password, bool teacher)
        {
            //Parsing variables from Application to the Json Properties.
            Name = name;
            Email = email;
            Password = password;
            IsTeacher = teacher;
        }
    }
}
