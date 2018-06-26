using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    class AuthenticateResponse
    {
        [JsonProperty("token")] //Key value based on Json Schema, recognises this is a token from the paired JSON key from the API.
        public string Token { get; set; }
    }
}
