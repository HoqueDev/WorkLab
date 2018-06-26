using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    class SubmitHomework
    {
        //Each JsonProperty creates a Key which the API can use to recognise what piece of data it is.
        [JsonProperty("url")]
        public string Url { get; set; }

        public SubmitHomework(string url)
        {
            Url = url;
        }
    }
}
