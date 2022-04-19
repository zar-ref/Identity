using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Identity.DTO.WebSocketModels;

namespace Identity.Infrastructure.WebSocket.Client
{
    public class WsClient
    {
        #region Fields
        private Socket client = null;
        public event ClientOnConnect OnConnect = null;
        public event ClientOnConnectError OnConnectError = null;
        public event ServerOnReceive OnReceive = null;
        public event ServerOnReceiveError OnReceiveError = null;
        public event ClientOnSend OnSend = null;
        public event ClientOnSendError OnSendError = null;
        public event ServerOnDisconnect OnDisconnect = null;
        #endregion

        public bool Connected
        {
            get
            {
                return client.Connected;
            }
        }

        #region Connect
        /// <summary>
        /// Connect to server
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        public void Connect(string ipaddress, int port)
        {
            Connect(new IPEndPoint(IPAddress.Parse(ipaddress), port));
            //Send Ack saying we are the master application socket
            Send(new SocketMessageDTO()
            {
                MessageType = "ACK",
                CustomerType = "zarref"

            });
        }

        /// <summary>
        /// Connect to server
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        public void Connect(IPAddress ipaddress, int port)
        {
            Connect(new IPEndPoint(ipaddress, port));
        }

        /// <summary>
        /// Connect to server
        /// </summary>
        /// <param name="ep"></param>
        public void Connect(IPEndPoint ep)
        {
            try
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.BeginConnect(ep, new AsyncCallback(BeginConnect), null);
            }
            catch (Exception ex)
            {
                if (OnConnectError != null)
                    OnConnectError.Invoke(ex);
            }
        }

        /// <summary>
        /// Works when connected to server
        /// </summary>
        /// <param name="ar"></param>
        private void BeginConnect(IAsyncResult ar)
        {
            try
            {
                if (client != null)
                {
                    ClientInfoDTO clientinfo = new ClientInfoDTO()
                    {
                        socket = client
                    };
                    client.EndConnect(ar);

                    if (OnConnect != null)
                        OnConnect.Invoke();

                    client.BeginReceive(clientinfo.receivedata, 0, clientinfo.receivedata.Length, SocketFlags.None, new AsyncCallback(BeginReceive), clientinfo);
                }
            }
            catch (Exception ex)
            {
                if (OnConnectError != null)
                    OnConnectError.Invoke(ex);
            }
        }
        #endregion

        #region Receive
        /// <summary>
        /// Receive data from server
        /// </summary>
        /// <param name="ar"></param>
        private void BeginReceive(IAsyncResult ar)
        {
            try
            {
                if (client != null)
                {
                    ClientInfoDTO clientinfo = (ClientInfoDTO)ar.AsyncState;
                    if (OnReceive != null & client.Connected)
                    {
                        int receivelength = client.EndReceive(ar);
                        if (receivelength > 0)
                        {
                            OnReceive.Invoke(Encoding.UTF8.GetString(clientinfo.receivedata, 0, receivelength));
                        }
                        else
                        {
                            if (OnDisconnect != null)
                                OnDisconnect.Invoke();
                        }

                        if (client != null)
                            client.BeginReceive(clientinfo.receivedata, 0, clientinfo.receivedata.Length, SocketFlags.None, new AsyncCallback(BeginReceive), clientinfo);
                    }
                }
            }
            catch (Exception ex)
            {
                if (OnReceiveError != null & (ex as SocketException).ErrorCode != 10054)
                    OnReceiveError.Invoke(ex);
                else if (!client.Connected & (ex as SocketException).ErrorCode == 10054)
                {
                    if (OnDisconnect != null)
                        OnDisconnect.Invoke();
                }
            }
        }
        #endregion

        #region Send
        /// <summary>
        /// Send string data to server
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            Send(Encoding.UTF8.GetBytes(message.Trim()));
        }

        /// <summary>
        /// Send byte data to server
        /// </summary>
        /// <param name="byteArray"></param>
        public void Send(byte[] byteArray)
        {
            try
            {
                if (client != null & byteArray.Length > 0)
                {
                    ClientInfoDTO clientinfo = new ClientInfoDTO()
                    {
                        socket = client,
                        senddata = byteArray
                    };

                    clientinfo.socket.BeginSend(clientinfo.senddata, 0, clientinfo.senddata.Length, SocketFlags.None, new AsyncCallback(BeginSend), clientinfo);
                }
            }
            catch (Exception ex)
            {
                if (OnSendError != null)
                    OnSendError.Invoke(ex);
            }
        }

        /// <summary>
        /// Works when sending message
        /// </summary>
        /// <param name="ar"></param>
        private void BeginSend(IAsyncResult ar)
        {
            try
            {
                if (client != null)
                {
                    ClientInfoDTO clientinfo = (ClientInfoDTO)ar.AsyncState;
                    clientinfo.socket.EndSend(ar);
                    if (OnSend != null)
                        OnSend.Invoke(Encoding.UTF8.GetString(clientinfo.senddata, 0, clientinfo.senddata.Length));
                }
            }
            catch (Exception ex)
            {
                if (OnSendError != null)
                    OnSendError.Invoke(ex);
            }
        }



        public void Send(SocketMessageDTO socketMessage)
        {
            Send(socketMessage.WriteToJson());
        }


        #endregion

        /// <summary>
        /// Disconnect client
        /// </summary>
        public void Disconnect()
        {
            if (client != null)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                client = null;
            }
        }

        /// <summary>
        /// Dispose client
        /// </summary>
        public void Dispose()
        {
            Disconnect();
        }

    }
}
