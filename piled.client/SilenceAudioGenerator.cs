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
    public static class SilenceAudioGenerator
    {
        public static async Task GenerateSilenceAsync(CancellationToken cancellationToken)
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
                            await Task.Delay(500, cancellationToken).ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (TaskCanceledException)
            {
            }
        }
    }
}
