﻿using Identity.DTO.WebSocketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.WebSocket.Client
{
    public class WsPromoteItClient
    {
        private static WsPromoteItClient instance = null;
        public static WsPromoteItClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WsPromoteItClient();
                }
                return instance;
            }
        }


        public  WsClient Client { get; set; }
        public WsPromoteItClient()
        {
            Client = new WsClient();

            Client.Connect("127.0.0.1", 7788); 

            //Send Ack saying we are the master application socket
            Client.Send(new SocketMessageDTO()
            {
                MessageType = "ACK",
                CustomerType = "zarref"

            });
        }

    }
}
