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
        public const string PiEndpointAddress = "192.168.2.108";
        public const int PiPort = 11035;

        public static void Main(string[] args)
        {
            Console.WriteLine("piledclient starting up. ");

            Console.WriteLine("press enter to start");
            Console.ReadLine();

            var conn = new PiConnection(PiEndpointAddress, PiPort);

            var colors = new List<RgbColor>
            {
                RgbColor.White,
                RgbColor.Green,
                RgbColor.Red,
                RgbColor.Blue,
                RgbColor.Black
            };

            var canvas = new RgbCanvas(64, 32);

            try
            {
                while (true)
                {
                    foreach (var color in colors)
                    {
                        CheckForExit();
                        canvas.Fill(color);
                        conn.SendBytes(canvas.ToBytes());
                        Thread.Sleep(300);
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                canvas.Fill(RgbColor.Black);
                conn.SendBytes(canvas.ToBytes());
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
