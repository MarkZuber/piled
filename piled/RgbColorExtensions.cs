using System;
using System.Collections.Generic;
using System.Text;

namespace piled
{
    public static class RgbColorExtensions
    {
        public static ConsoleColor ToConsoleColor(this RgbColor rgbColor)
        {
            if (rgbColor.R == 0 && rgbColor.G == 0 && rgbColor.B == 0)
            {
                return ConsoleColor.Black;
            }
            if (rgbColor.R == 255 && rgbColor.G == 255 && rgbColor.B == 255)
            {
                return ConsoleColor.White;
            }
            if (rgbColor.R == 255 && rgbColor.G == 0 && rgbColor.B == 0)
            {
                return ConsoleColor.Red;
            }
            if (rgbColor.R == 0 && rgbColor.G == 255 && rgbColor.B == 0)
            {
                return ConsoleColor.Green;
            }
            if (rgbColor.R == 0 && rgbColor.G == 0 && rgbColor.B == 255)
            {
                return ConsoleColor.Blue;
            }

            // todo: other colors...
            return ConsoleColor.Red;
        }
    }
}
