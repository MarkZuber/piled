using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace piledclient
{
    public class PiConnection
    {
        private readonly IPAddress _broadcast;
        private readonly string _targetAddress;
        private readonly int _targetPort;
        private readonly Socket _socket;

        public PiConnection(string targetAddress, int targetPort)
        {
            _targetAddress = targetAddress;
            _targetPort = targetPort;

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _broadcast = IPAddress.Parse(_targetAddress);
        }

        public void SendBytes(byte[] bytes)
        {
            IPEndPoint ep = new IPEndPoint(_broadcast, _targetPort);
            _socket.SendTo(bytes, ep);
        }
    }
}
