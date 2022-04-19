using Identity.DTO.WebSocketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.WebSocket.Client
{
    public class WsPromoteItClient
    {
      
        public  WsClient Client { get; set; }
        public WsPromoteItClient(string ip, int port)
        {
            Client = new WsClient();

            Client.Connect(ip, port);
        }
    }
}
