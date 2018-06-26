using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    public class User : ModelBase
    {
        //Each JsonProperty creates a Key which the API can use to recognise what piece of data it is.
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        
    }
}
