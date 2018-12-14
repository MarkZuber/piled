using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using piled;
using piled.client;

namespace piledclient
{
    public static class Program
    {
        public const string PiEndpointAddress = "192.168.2.108";
        public const int PiPort = 11035;

        private static readonly CancellationTokenSource Source = new CancellationTokenSource();

        private static void ReadKeys()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (!Source.IsCancellationRequested && !Console.KeyAvailable && key.Key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(true);
            }

            Source.Cancel();
        }

        public static async Task Main(string[] args)
        {
            Console.WriteLine("piledclient starting up. ");

            try
            {
                //Console.CancelKeyPress += (sender, eventArgs) => { Source.Cancel(); };
                var taskKeys = new Task(ReadKeys);
                taskKeys.Start();

                var renderer = new UdpRenderer(PiEndpointAddress, PiPort);
                // var activity = new SimpleFillDisplayActivity();
                // var activity = new WaveCaptureDisplayActivity();
                var activity = new BounceAndFillDisplayActivity();
                await activity.ExecuteAsync(renderer, Source.Token);

                var tasks = new[] {taskKeys};
                Task.WaitAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
