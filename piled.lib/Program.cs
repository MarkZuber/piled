﻿using System;
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
            const int delayCycleMs = 500;
            const int pixelCycleMs = 1;

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
                            CheckForExit();

                            matrix.Fill(color);
                            await Task.Delay(delayCycleMs).ConfigureAwait(false);

                            matrix.Clear();

                            // don't do this on black...
                            if (color.R != 0 || color.G != 0 || color.B != 0)
                            {
                                for (int y = 0; y < matrix.Height; y++)
                                {
                                    if (y % 2 == 0)
                                    {
                                        for (int x = 0; x < matrix.Width; x++)
                                        {
                                            CheckForExit();

                                            matrix.Clear();
                                            matrix.SetPixel(x, y, color);
                                            await Task.Delay(pixelCycleMs).ConfigureAwait(false);
                                        }
                                    }
                                    else
                                    {
                                        for (int x = matrix.Width - 1; x >= 0; x--)
                                        {
                                            CheckForExit();

                                            matrix.Clear();
                                            matrix.SetPixel(x, y, color);
                                            await Task.Delay(pixelCycleMs).ConfigureAwait(false);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (TaskCanceledException)
            {
            }
        }

        private static void CheckForExit()
        {
            if (Console.KeyAvailable)
            {
                throw new TaskCanceledException();
            }
        }
    }
}
