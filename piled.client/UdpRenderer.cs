using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using piledclient;

namespace piled.client
{
    public class UdpRenderer : IRenderer
    {
        private readonly PiConnection _piConnection;

        public UdpRenderer(string endpointAddress, int endpointPort)
        {
            _piConnection = new PiConnection(endpointAddress, endpointPort);
        }

        public void Render(RgbCanvas rgbCanvas)
        {
            _piConnection.SendBytes(rgbCanvas.ToBytes());
        }
    }
}
