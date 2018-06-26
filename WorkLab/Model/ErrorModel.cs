using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    
    class ErrorModel
    {
        //Each JsonProperty creates a Key which the API can use to recognise what piece of data it is.
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public ErrorModel() : this("unknown", "unknown error")
        {
        }

        public ErrorModel(string type, string message)
        {
            //Parsing Error messages from the API, helps to debug on client-side.
            Type = type;
            Message = message;
        }
    }
}
