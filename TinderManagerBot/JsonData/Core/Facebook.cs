using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.JsonData.Core
{
    class Facebook
    {
        public List<object> common_connections { get; set; }
        public List<object> common_interests { get; set; }
        public int? connection_count { get; set; }
    }
}
