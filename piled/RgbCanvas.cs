using System;
using System.Collections.Generic;
using System.Text;

namespace piled
{
    public class RgbCanvas
    {
        private readonly RgbColor[,] _canvas;

        public RgbCanvas(int width, int height)
        {
            Width = width;
            Height = height;
            _canvas = new RgbColor[width,height];
            Fill(RgbColor.Black);
        }

        public int Width { get; }
        public int Height { get; }

        public void SetPixel(int x, int y, RgbColor color)
        {
            ValidateCoordinates(x, y);
            _canvas[x, y] = color;
        }

        public RgbColor GetPixel(int x, int y)
        {
            ValidateCoordinates(x, y);
            return _canvas[x, y];
        }

        public void Fill(RgbColor color)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _canvas[x, y] = color;
                }
            }
        }

        private void ValidateCoordinates(int x, int y)
        {
            if (x < 0 || x >= Width)
            {
                throw new ArgumentException($"x ({x}) must be in the range [0,{Width})");
            }

            if (y < 0 || y >= Height)
            {
                throw new ArgumentException($"y ({y}) must be in the range [0,{Height})");
            }
        }
    }
}
