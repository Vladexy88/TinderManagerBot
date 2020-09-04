using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.JsonData
{
    class Data
    {
        // response : {"meta":{"status":200},"data":{"refresh_token":"eyJhbGciOiJIUzI1NiJ9.NzkyNTI4ODEwODk.zFS_Dueu9DQ33Fx3c69QCptcWxAThYmts5wsatqNJmk","validated":true}}

        public string refresh_token { get; set; }
        public bool? validated { get; set; }
        public string _id { get; set; }
        public string api_token { get; set; }
        public bool? is_new_user { get; set; }
    }
}
