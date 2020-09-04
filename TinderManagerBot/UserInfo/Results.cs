using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.UserInfo
{
    class Results
    {
        public string bio { get; set; }
        public DateTime birth_date { get; set; }
        public string birth_date_info { get; set; }
        public int distance_mi { get; set; }
        public string name { get; set; }
        public List<Photo> Photos { get; set; }
        public string _id { get; set; }
    }
}
