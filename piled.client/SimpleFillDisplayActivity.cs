using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace piled.client
{
    public class SimpleFillDisplayActivity : AbstractDisplayActivity
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

            while (true)
            {
                foreach (var color in colors)
                {
                    CheckForExit();
                    Canvas.Fill(color);
                    Render();
                    await Task.Delay(300, cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }
}
