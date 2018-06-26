using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    class CreateHomework
    {
        //Each JsonProperty creates a Key which the API can use to recognise what piece of data it is.
        [JsonProperty("class_id")]
        public int ClassId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        public CreateHomework(int class_id, string name, string url)
        {
            //Parsing variables from Application to the Json Properties.
            ClassId = class_id;
            Name = name;
            Url = url;
        }
    }
}
