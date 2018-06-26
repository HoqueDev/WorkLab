using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    class StudentHomework
    {
        //Each JsonProperty creates a Key which the API can use to recognise what piece of data it is.
        [JsonProperty("student_id")]
        public int StudentId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
