using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using piled;

namespace piledserver
{
    public static class Program
    {
        public const int ListenPort = 11035;

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting up piled server");

            var matrix = RgbMatrixFactory.Create();

            using (UdpClient listener = new UdpClient(ListenPort))
            {
                IPEndPoint groupEndpoint = new IPEndPoint(IPAddress.Any, ListenPort);

                try
                {
                    while (true)
                    {
                        byte[] bytes = listener.Receive(ref groupEndpoint);
                        Console.WriteLine($"Received {bytes.Length} bytes from {groupEndpoint}");
                        matrix.SetCanvas(RgbCanvas.FromBytes(matrix.Width, matrix.Height, bytes));
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
