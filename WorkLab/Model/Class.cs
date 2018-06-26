using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab.Model
{
    public class Class : ModelBase
    {
        //private int? lastHomework;

        [JsonProperty("name")] //fetches name from API
        public string Name { get; set; }

        [JsonProperty("teacher_id")] //fetches teacher_id from API
        public int TeacherId { get; set; }

        [JsonProperty("students")] //fetches Students from API
        public List<User> Students { get; set; }

        [JsonProperty("last_homework")] //fetches last_homework from API
        public int? LastHomework { get; set; }

        //GetSet is used to fetch the data and then assign it an appropiate datatype.
    }
}
