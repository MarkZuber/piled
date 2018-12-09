using System;
using System.Collections.Generic;
using System.Text;

namespace piled
{
    public class ConsoleRgbMatrix : IRgbMatrix
    {
        public ConsoleRgbMatrix(int width, int height)
        {
            Width = width;
            Height = height;

            Console.SetWindowSize(Width, Height);
            Clear();
        }

        public int Width { get; }
        public int Height { get; }
        public void Clear()
        {
            Console.Clear();
        }

        public void Fill(RgbColor color)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.ForegroundColor = color.ToConsoleColor();
                    Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        public void SetPixel(int x, int y, RgbColor color)
        {
        }

        public void SetCanvas(RgbCanvas canvas)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.ForegroundColor = canvas.GetPixel(x, y).ToConsoleColor();
                    Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        public void SetCanvasAt(int x, int y, RgbCanvas canvas)
        {
        }

        public void SetPartialCanvasAt(int targetX, int targetY, RgbCanvas canvas, int sourceX, int sourceY, int sourceWidth,
            int targetWidth)
        {
        }

        public void SetPwmBits(byte bits)
        {
        }

        public void SetWriteCycles(byte writeCycles)
        {
        }

        private void ReleaseUnmanagedResources()
        {
            Console.ResetColor();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~ConsoleRgbMatrix()
        {
            ReleaseUnmanagedResources();
        }
    }
}
