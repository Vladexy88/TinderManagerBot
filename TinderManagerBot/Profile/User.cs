using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.Profile
{
    class User
    {
        public string name { get; set; }
        public string phone_id { get; set; }
        public string _id { get; set; }
        public List<Photo> photos { get; set; }
        public DateTime ping_time { get; set; }
    }
}
