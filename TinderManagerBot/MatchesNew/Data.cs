using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.MatchesNew
{
    class Data
    {
        public List<Matches> Matches { get; set; }
        public string next_page_token { get; set; } = "";
    }
}
