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

     
        public WsPromoteItClient()
        {
            Client = new WsClient();

            Client.Connect("127.0.0.1", 7788); 

            
        }
        public WsPromoteItClient(string ip, int port)
        {
            Client = new WsClient();

            Client.Connect(ip, port);
        }
    }
}
