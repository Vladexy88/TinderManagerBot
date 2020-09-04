using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.MatchesNew
{
    class Person
    {
        public DateTime birth_date { get; set; }
        public int gender { get; set; }
        public string name { get; set; }
        public string _id { get; set; }
        public List<Photo> photos { get; set; }
    }
}
