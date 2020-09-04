using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.JsonData
{
    class Meta
    {
        //{"meta":{"status":200},"data":{"refresh_token":"eyJhbGciOiJIUzI1NiJ9.NzkzMTIwMzc0ODA.BpWqjC8My2urKwEWTEdbbqQlERXLgXRYfMdpNxCg3R0",
        //"onboarding_token":"a37e13d712adcc30a2920f03d4b11bc0ed4d7a3be9181028b9c02e8b932d769dkoe","is_new_user":true}}
        public int? status { get; set; }
       
    }
}
