using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace piled
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            const int delayCycleMs = 1000;

            Console.WriteLine("PILed starting up...");

            try
            {
                using (var matrix = RgbMatrixFactory.Create())
                {
                    Console.WriteLine("Press ENTER to stop running...");
                    var colors = new List<RgbColor>
                    {
                        RgbColor.White,
                        RgbColor.Green,
                        RgbColor.Red,
                        RgbColor.Blue,
                        RgbColor.Black
                    };

                    while (true)
                    {
                        foreach (var color in colors)
                        {
                            if (Console.KeyAvailable)
                            {
                                throw new OperationCanceledException();
                            }

                            matrix.Fill(color);
                            await Task.Delay(delayCycleMs).ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {

            }
        }
    }
}
