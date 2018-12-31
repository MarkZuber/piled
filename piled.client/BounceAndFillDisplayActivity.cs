using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace piled.client
{
    public class BounceAndFillDisplayActivity : AbstractDisplayActivity
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var colors = new List<RgbColor>
            {
                RgbColor.White,
                RgbColor.Green,
                RgbColor.Red,
                RgbColor.Blue,
                RgbColor.Black
            };

            const int delayCycleMs = 500;
            const int pixelCycleMs = 1;

            while (true)
            {
                foreach (var color in colors)
                {
                    CheckForExit();

                    Canvas.Fill(color);
                    Render();
                    await Task.Delay(delayCycleMs, cancellationToken).ConfigureAwait(false);

                    Canvas.Fill(RgbColor.Black);
                    Render();

                    // don't do this on black...
                    if (color.R != 0 || color.G != 0 || color.B != 0)
                    {
                        for (int y = 0; y < Canvas.Height; y++)
                        {
                            if (y % 2 == 0)
                            {
                                for (int x = 0; x < Canvas.Width; x++)
                                {
                                    CheckForExit();

                                    Canvas.Fill(RgbColor.Black);
                                    Render();

                                    Canvas.SetPixel(x, y, color);
                                    Render();
                                    await Task.Delay(pixelCycleMs, cancellationToken).ConfigureAwait(false);
                                }
                            }
                            else
                            {
                                for (int x = Canvas.Width - 1; x >= 0; x--)
                                {
                                    CheckForExit();

                                    Canvas.Fill(RgbColor.Black);
                                    Render();
                                    Canvas.SetPixel(x, y, color);
                                    Render();
                                    await Task.Delay(pixelCycleMs, cancellationToken).ConfigureAwait(false);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
