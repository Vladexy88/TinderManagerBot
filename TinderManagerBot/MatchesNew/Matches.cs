using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.MatchesNew
{
    class Matches
    {
        public bool closed { get; set; }
        public int common_friend_count { get; set; }
        public int common_like_count { get; set; }
        public DateTime created_date { get; set; }
        public bool dead { get; set; }
        public bool following { get; set; }
        public string id { get; set; }
        public DateTime last_activity_date { get; set; }
        public List<Messages> Messages { get; set; }
        public string _id { get; set; }
        public Person person { get; set; }
    }
}
