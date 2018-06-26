using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    class Homework : ModelBase
    {
        //Each JsonProperty creates a Key which the API can use to recognise what piece of data it is.
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("class_id")]
        public int ClassId;

        [JsonProperty("class_name")]
        public string ClassName;

        [JsonProperty("teacher_id")]
        public int TeacherId;

        [JsonProperty("teacher_name")]
        public string TeacherName;

        [JsonProperty("url")]
        public string Url;

        [JsonProperty("submitted")]
        public bool Submitted;
    }
}
