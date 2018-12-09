using System;
using System.Collections.Generic;
using System.Text;

namespace piled
{
    public static class RgbMatrixFactory
    {
        public static IRgbMatrix Create()
        {
            string env = Environment.GetEnvironmentVariable("LOGNAME");
            if (string.IsNullOrWhiteSpace(env))
            {
                return new ConsoleRgbMatrix(64, 32);
            }
            else
            {
                return new PiRgbMatrix(2);
            }
        }
    }
}
