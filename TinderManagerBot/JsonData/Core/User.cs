using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.JsonData.Core
{
    class User
    {
        public string bio { get; set; }
        public DateTime birth_date { get; set; }
        public City City { get; set; }
        public int? gender { get; set; }
        public List<object> jobs { get; set; }
        public string name { get; set; }
        public List<Photos> photos { get; set; }
        public string url { get; set; }
        public List<object> schools { get; set; }
        public string _id { get; set; }
        public bool? show_gender_on_profile { get; set; }
        public bool? is_traveling { get; set; }
        public bool? new_user_badge_enabled { get; set; }
        public bool? hide_age { get; set; }
        public bool? hide_distance { get; set; }
    }
}
