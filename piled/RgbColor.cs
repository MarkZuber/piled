using System;
using System.Collections.Generic;
using System.Text;

namespace piled
{
    public class RgbColor
    {
        public RgbColor()
        {
            R = 0;
            G = 0;
            B = 0;
        }

        public RgbColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static RgbColor Black => new RgbColor(0, 0, 0);
        public static RgbColor White => new RgbColor(255, 255, 255);
        public static RgbColor Red => new RgbColor(255, 0, 0);
        public static RgbColor Green => new RgbColor(0, 255, 0);
        public static RgbColor Blue => new RgbColor(0, 0, 255);

        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
    }
}
