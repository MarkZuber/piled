using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piled.client
{
    public class FFT
    {
        private readonly int _n;
        private readonly int _m;

        // Lookup tables. Only need to recompute when size of FFT changes.
        private readonly float[] _cos;
        public readonly float[] _sin;

        public FFT(int n) 
        {
            _n = n;
            _m = Convert.ToInt32(Math.Log(n) / Math.Log(2.0));

            // Make sure n is a power of 2
            if (n != (1 << _m))
            {
                throw new InvalidOperationException("FFT length must be power of 2");
            }

            // precompute tables
            _cos = new float[n / 2];
            _sin = new float[n / 2];

            for (int i = 0; i < n / 2; i++) 
            {
                _cos[i] = Convert.ToSingle(Math.Cos(-2.0 * Math.PI * Convert.ToDouble(i) / Convert.ToDouble(n)));
                _sin[i] = Convert.ToSingle(Math.Sin(-2.0 * Math.PI * Convert.ToDouble(i) / Convert.ToDouble(n)));
            }

        }

        public void Fft(float[] x)
        {
            float[] y = new float[x.Length];
            for (int i = 0; i < y.Length; i++)
            {
                y[i] = 0.0f;
            }
        }

        public void Fft(float[] x, float[] y) 
        {
            int i, j, k, n1, n2, a;
            float c, s, t1, t2;

            // Bit-reverse
            j = 0;
            n2 = _n / 2;
            for (i = 1; i < _n - 1; i++) 
            {
                n1 = n2;
                while (j >= n1) 
                {
                    j = j - n1;
                    n1 = n1 / 2;
                }
                j = j + n1;

                if (i < j) 
                {
                    t1 = x[i];
                    x[i] = x[j];
                    x[j] = t1;
                    t1 = y[i];
                    y[i] = y[j];
                    y[j] = t1;
                }
            }

            // FFT
            n1 = 0;
            n2 = 1;

            for (i = 0; i < _m; i++) 
            {
                n1 = n2;
                n2 = n2 + n2;
                a = 0;

                for (j = 0; j < n1; j++) 
                {
                    c = _cos[a];
                    s = _sin[a];
                    a += 1 << (_m - i - 1);

                    for (k = j; k < _n; k = k + n2) 
                    {
                        t1 = (c * x[k + n1]) - (s * y[k + n1]);
                        t2 = (s * x[k + n1]) + (c * y[k + n1]);
                        x[k + n1] = x[k] - t1;
                        y[k + n1] = y[k] - t2;
                        x[k] = x[k] + t1;
                        y[k] = y[k] + t2;
                    }
                }
            }
        }
    }
}
