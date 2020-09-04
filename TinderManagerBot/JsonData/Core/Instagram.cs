using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.JsonData.Core
{
    class Instagram
    {
        public DateTime last_fetch_time { get; set; }
        public bool? completed_initial_fetch { get; set; }
       // public List<Photo2> photos { get; set; }
        public int? media_count { get; set; }
        public string profile_picture { get; set; }
    }
}

