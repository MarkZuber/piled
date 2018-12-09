using System;
using System.Collections.Generic;
using System.Text;

namespace piled
{
    public class RgbCanvas
    {
        private readonly RgbColor[,] _canvas;

        public RgbCanvas() : this(64, 32)
        {
        }

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

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[Width * Height * 3];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int startOffset = ((y * Height) + x) * 3;
                    var color = _canvas[x, y];
                    bytes[startOffset] = color.R;
                    bytes[startOffset + 1] = color.G;
                    bytes[startOffset + 2] = color.B;
                }
            }

            return bytes;
        }

        public static RgbCanvas FromBytes(int width, int height, byte[] bytes)
        {
            if (width * height * 3 != bytes.Length)
            {
                throw new ArgumentException($"width {width} and height {height} and 3 colors per pixel don't match bytes length {bytes.Length}");
            }

            var canvas = new RgbCanvas(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int startOffset = ((y * height) + x) * 3;
                    var color = new RgbColor(bytes[startOffset], bytes[startOffset + 1], bytes[startOffset + 2]);
                    canvas.SetPixel(x, y, color);
                }
            }

            return canvas;
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
