using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.JsonData.Core
{
    class Photos
    {
        public string id { get; set; }
        public string last_update_time { get; set; }
        public List<ProcessedFiles> processedFiles { get; set; }
    }
}
