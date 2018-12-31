using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;

namespace piled.client
{
    public abstract class AbstractAudioCaptureDisplayActivity : AbstractDisplayActivity
    {
        protected abstract void CaptureDataAvailable(WaveInEventArgs args);

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var task = SilenceAudioGenerator.GenerateSilenceAsync(cancellationToken);

            var capture = new WasapiLoopbackCapture();

            capture.DataAvailable += (sender, args) => CaptureDataAvailable(args);
            capture.RecordingStopped += (sender, args) => capture.Dispose();
            capture.StartRecording();
            while (capture.CaptureState != NAudio.CoreAudioApi.CaptureState.Stopped)
            {
                try
                {
                    await Task.Delay(500, cancellationToken).ConfigureAwait(false);
                }
                catch (TaskCanceledException)
                {
                    capture.StopRecording();
                }
            }
        }
    }
}
