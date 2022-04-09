using Identity.DTO.WebSocketModels;
using Identity.Models.Configuration;
using Microsoft.Extensions.Configuration; 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.WebSocket.Client
{
    public  class WsManager
    {

        private static WsManager instance;
        public static WsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WsManager();
                }
                return instance;
            }
        }


        public Dictionary<WsPromoteItClient, List<string>> ContextNamesByWebSocketClient { get; set; } = new Dictionary<WsPromoteItClient, List<string>>();



        public WsManager() { }
        
       
        public void Init(IConfiguration config)
        {
     
            var webSocketServerConfigurations = config.GetSection("WebSocketServers").Get<WebSocketServerSetting[]>();
            foreach (var webSocketServerconfig in webSocketServerConfigurations)
            {
                WsPromoteItClient socketClient = new WsPromoteItClient(webSocketServerconfig.IP,webSocketServerconfig.Port);
                ContextNamesByWebSocketClient.Add(socketClient, webSocketServerconfig.Contexts);
            } 
        }

        public void BroadcastMessage(string context, SocketMessageDTO message)
        {
            foreach (var socketClient in ContextNamesByWebSocketClient)
            {
                if(socketClient.Value.Any(_ctx => _ctx== context))
                {
                    socketClient.Key.Client.Send(message);
                    return;
                }
            }
        }
    }
}
