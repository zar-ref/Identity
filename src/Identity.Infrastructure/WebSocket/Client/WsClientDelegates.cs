using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.WebSocket.Client
{
    public delegate void ClientOnConnect();

    public delegate void ClientOnConnectError(Exception ex);

    public delegate void ServerOnReceive(string message);

    public delegate void ServerOnReceiveError(Exception ex);

    public delegate void ClientOnSend(string message);

    public delegate void ClientOnSendError(Exception ex);

    public delegate void ServerOnDisconnect();
}
