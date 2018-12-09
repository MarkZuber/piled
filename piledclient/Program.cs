using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using piled;

namespace piledclient
{
    public static class Program
    {
        // public const string PiEndpointAddress = "192.168.2.108";
        public const string PiEndpointAddress = "192.168.2.197";
        public const int PiPort = 11035;

        public static void Main(string[] args)
        {
            Console.WriteLine("piledclient starting up. ");

            Console.WriteLine("press enter to start");
            Console.ReadLine();

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse(PiEndpointAddress);

            var colors = new List<RgbColor>
            {
                RgbColor.White,
                RgbColor.Green,
                RgbColor.Red,
                RgbColor.Blue,
                RgbColor.Black
            };

            // create just to get width/height
            var matrix = RgbMatrixFactory.Create();
            var canvas = new RgbCanvas(matrix.Width, matrix.Height);

            try
            {
                while (true)
                {
                    foreach (var color in colors)
                    {
                        CheckForExit();
                        canvas.Fill(color);

                        IPEndPoint ep = new IPEndPoint(broadcast, PiPort);
                        s.SendTo(canvas.ToBytes(), ep);
                        Thread.Sleep(300);
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private static void CheckForExit()
        {
            if (Console.KeyAvailable)
            {
                throw new OperationCanceledException();
            }
        }
    }
}
