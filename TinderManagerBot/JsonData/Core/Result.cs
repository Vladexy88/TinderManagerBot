using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.JsonData.Core
{
    class Result
    {
        public string content_hash { get; set; }
        public int? distance_mi { get; set; }
        public Facebook facebook { get; set; }
        public int s_number { get; set; }
        public Spotify spotify { get; set; }
        public string type { get; set; }
        public User user { get; set; }
        public Instagram instagram { get; set; }

    }
}
