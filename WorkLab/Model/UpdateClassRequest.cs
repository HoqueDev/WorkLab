using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    class UpdateClassRequest
    {
        //Each JsonProperty creates a Key which the API can use to recognise what piece of data it is.
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("add_student")]
        public int? AddStudent { get; set; }

        [JsonProperty("remove_student")]
        public int? RemoveStudent { get; set; }
    }
}
