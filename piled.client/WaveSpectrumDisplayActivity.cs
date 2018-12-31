using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;

namespace piled.client
{
    public class WaveSpectrumDisplayActivity : AbstractAudioCaptureDisplayActivity
    {
        private const int ChunkSize = 32;
        private readonly float[] _windowed = new float[ChunkSize];
        private readonly float[] _hanning = new float[ChunkSize];
        private readonly FFT _fft = new FFT(ChunkSize);

        private const int GreenMax = 18;
        private const int YellowMax = 26;

        private int _currentWindowOffset = 0;

        public WaveSpectrumDisplayActivity()
        {
            for (int i = 0; i < ChunkSize; i++)
            {
                _hanning[i] = Convert.ToSingle((1.0 - Math.Cos(Convert.ToDouble(i) * 2.0 * Math.PI / (ChunkSize - 1.0))) / 2.0);
            }
        }

        protected override void CaptureDataAvailable(WaveInEventArgs args)
        {
            var buffer = new WaveBuffer(args.Buffer);
            // interpret as 32 bit floating point audio
            for (int index = 0; index < args.BytesRecorded / 4; index++)
            {
                float sample = Math.Abs(buffer.FloatBuffer[index]);

                _windowed[_currentWindowOffset] = sample;
                _currentWindowOffset++;

                if (_currentWindowOffset == ChunkSize)
                {
                    // render time!

                    for (int i = 0; i < ChunkSize; i++)
                    {
                        _windowed[i] = _windowed[i] * _hanning[i];
                    }

                    // apply fft to _windowed
                    _fft.Fft(_windowed);

                    Canvas.Fill(RgbColor.Black);

                    const float scalingFactor = 1.8f;

                    // get magnitude (linear scale) of first half values and plot
                    for (int i = 0; i < ChunkSize; i++)
                    {
                        float mag = Math.Abs(_windowed[i]) * scalingFactor;
                        int totalHeight = Convert.ToInt32(mag * Convert.ToDouble(Canvas.Width));

                        Canvas.DrawLineY(i, RgbColor.Green, 0, Math.Min(GreenMax, totalHeight));
                        if (totalHeight > GreenMax)
                        {
                            Canvas.DrawLineY(i, RgbColor.Blue, GreenMax, Math.Min(YellowMax, totalHeight));
                        }

                        if (totalHeight > YellowMax)
                        {
                            Canvas.DrawLineY(i, RgbColor.Red, YellowMax, Math.Min(Canvas.Width, totalHeight));
                        }
                    }

                    Render();

                    _currentWindowOffset = 0;
                }
            }

        }
    }
}
