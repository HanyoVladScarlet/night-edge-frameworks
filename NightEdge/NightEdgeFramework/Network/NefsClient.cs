using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace NightEdgeFramework.Network
{
    public class NefsClient
    {
        public void Connect()
        {
            string ip = "192.168.1.2";
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), 5000);

            s.Connect(ep);
        }
    }  

    public class NefsNetSocket
    {
        private Socket targetSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        public void EstablishConnection()
        {
            
        }
    }
}
