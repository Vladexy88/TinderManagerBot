using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.MessagesInformation
{
    class Messages
    {
        public DateTime created_date { get; set; }
        public string from { get; set; }
        public string match_id { get; set; }
        public string message { get; set; }
        public DateTime sent_date { get; set; }
        public object timestamp { get; set; }
        public string to { get; set; }
        public string _id { get; set; }
    }
}
