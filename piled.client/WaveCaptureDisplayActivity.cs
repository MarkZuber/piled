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
    public class WaveCaptureDisplayActivity : AbstractDisplayActivity
    {
        private async Task GenerateSilenceAsync()
        {
            try
            {
                while (true)
                {
                    var signal = new SignalGenerator()
                    {
                        Gain = 0.0,
                        Frequency = 500,
                        Type = SignalGeneratorType.Sin
                    }.Take(TimeSpan.FromSeconds(20));

                    using (var wo = new WaveOutEvent())
                    {
                        wo.Init(signal);
                        wo.Play();
                        while (wo.PlaybackState == PlaybackState.Playing)
                        {
                            CheckForExit();
                            await Task.Delay(500);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        protected override async Task ExecuteAsync()
        {
            var task = GenerateSilenceAsync();

            var capture = new WasapiLoopbackCapture();

            capture.DataAvailable += (sender, args) =>
            {
                float max = 0;
                var buffer = new WaveBuffer(args.Buffer);
                // interpret as 32 bit floating point audio
                for (int index = 0; index < args.BytesRecorded / 4; index++)
                {
                    var sample = buffer.FloatBuffer[index];

                    // absolute value 
                    if (sample < 0)
                    {
                        sample = -sample;
                    }

                    // is this the max value?
                    if (sample > max)
                    {
                        max = sample;
                    }
                }

                int volHeight = Convert.ToInt32(Convert.ToDouble(Canvas.Height) * max);

                Console.WriteLine($"VolHeight: {volHeight}");

                for (int x = 0; x < volHeight; x++)
                {
                    Console.WriteLine($"Drawing {x} as green");
                    for (int y = 0; y < Canvas.Height; y++)
                    {
                        Canvas.SetPixel(x, y, RgbColor.Green);
                    }
                }
                for (int x = volHeight; x < Canvas.Width; x++)
                {
                    Console.WriteLine($"Drawing {x} as black");
                    for (int y = 0; y < Canvas.Height; y++)
                    {
                        Canvas.SetPixel(x, y, RgbColor.Black);
                    }
                }

                Render();
            };

            capture.RecordingStopped += (sender, args) => { capture.Dispose(); };
            capture.StartRecording();
            while (capture.CaptureState != NAudio.CoreAudioApi.CaptureState.Stopped)
            {
                try
                {
                    CheckForExit();
                }
                catch (OperationCanceledException)
                {
                    capture.StopRecording();
                }

                await Task.Delay(500);
            }
        }
    }
}
