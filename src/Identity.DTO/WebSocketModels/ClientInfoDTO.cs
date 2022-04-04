using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Identity.DTO.WebSocketModels
{
    public class ClientInfoDTO
    {
        public Socket socket = null;
        public byte[] receivedata;
        public byte[] senddata;

        public string ClientApplicationContextName { get; set; }
        public bool IsMasterApplication { get; set; } = false;
        public bool IsApplicationUser { get; set; } = false; //Users from the application, NOT FINAL CUSTOMERS

        public ClientInfoDTO()
        {
            receivedata = new byte[10485760]; //10MB
            senddata = new byte[10485760];
        }

        /// <summary>
        /// Get client endpoint
        /// </summary>
        public string Name
        {
            get
            {
                if (socket != null)
                    return socket.RemoteEndPoint.ToString();
                else
                    return "";
            }
        }

        public EndPoint GetEndPoint
        {
            get
            {
                if (socket != null)
                    return socket.RemoteEndPoint;
                else
                    return null;
            }
        }

        /// <summary>
        /// Get client ip address
        /// </summary>
        public string GetIPAddres
        {
            get
            {
                if (socket != null)
                    return socket.RemoteEndPoint.ToString().Split(':')[0];
                else
                    return string.Empty;
            }
        }

        public override string ToString()
        {
            if (socket != null)
                return socket.RemoteEndPoint.ToString();
            else
                return base.ToString();
        }
    }
}
