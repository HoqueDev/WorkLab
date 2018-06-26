using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    class CreateClassRequest
    {
        [JsonProperty("name")] //Sets Json property as Name, so API will know which property this belongs to.
        public string Name { get; set; }

        public CreateClassRequest(string name)
        {
            Name = name;
        }
    }
}
