using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.Matches
{
    class MatchesRootObject
    {
        public string matchId { get; set; }
        public string message { get; set; }
        public string otherId { get; set; }
        public string sessionId { get; set; }
        public string tempMessageId { get; set; }
        public string userId { get; set; }
    }
}
