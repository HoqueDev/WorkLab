using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    class ApiResponse<TModel>
    {
        //key-value pairs) on an object are defined using the properties keyword. 
        //The value of properties is an object, where each key is the name of a property and each value is a JSON schema used to validate that property
        [JsonProperty("response")] 
        public TModel Response { get; set; }
        [JsonProperty("error")]
        public ErrorModel Error { get; set; }

    }
}
