using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Models.Configuration
{


    public class WebSocketServerSetting
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public List<string> Contexts { get; set; }
    }
}
