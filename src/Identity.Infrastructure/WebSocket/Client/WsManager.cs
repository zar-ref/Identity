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


        public Dictionary<WsPromoteItClient, List<string>> ContextNamesByWobSocketClient { get; set; } = new Dictionary<WsPromoteItClient, List<string>>();



        public WsManager() { }
        
       
        public void Init(IConfiguration config)
        {
     
            var webSocketServers = config.GetSection("WebSocketServers").Get<WebSocketServerSetting[]>();
            foreach (var webSocketServer in webSocketServers)
            {
                WsPromoteItClient socketClient = new WsPromoteItClient
            } 
        }
    }
}
