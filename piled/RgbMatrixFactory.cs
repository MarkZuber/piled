using System;
using System.Collections.Generic;
using System.Text;

namespace piled
{
    public static class RgbMatrixFactory
    {
        public static IRgbMatrix Create()
        {
            return new ConsoleRgbMatrix(64, 32);
        }
    }
}
