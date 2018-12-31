using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace piled.client
{
    public class WaveVolumeDisplayActivity : AbstractAudioCaptureDisplayActivity
    {
        protected override void CaptureDataAvailable(WaveInEventArgs args)
        {
            float max = 0.0f;
            var buffer = new WaveBuffer(args.Buffer);
            // interpret as 32 bit floating point audio
            for (int index = 0; index < args.BytesRecorded / 4; index++)
            {
                var sample = Math.Abs(buffer.FloatBuffer[index]);

                // is this the max value?
                if (sample > max)
                {
                    max = sample;
                }
            }

            int volHeight = Convert.ToInt32(Convert.ToDouble(Canvas.Height) * max * 1.5);

            for (int x = 0; x < volHeight; x++)
            {
                Canvas.DrawLineX(x, RgbColor.Green);
            }
            Canvas.DrawLineX(volHeight, RgbColor.Red);
            for (int x = volHeight+1; x < Canvas.Width; x++)
            {
                for (int y = 0; y < Canvas.Height; y++)
                {
                    Canvas.SetPixel(x, y, RgbColor.Black);
                }
            }

            Render();
        }
    }
}
