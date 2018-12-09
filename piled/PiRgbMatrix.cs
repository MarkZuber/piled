using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace piled
{
    public class PiRgbMatrix : IRgbMatrix
    {
        private readonly IntPtr _matrix;

        public PiRgbMatrix(int numCells)
        {
            Validate.Range(numCells, 1, 2);

            Width = 32 * numCells;
            Height = 32;

            _matrix = PiNativeMethods.CreateMatrix(Height, numCells);

            Clear();
        }

        public int Width { get; }
        public int Height { get; }
        
        public void Clear()
        {
            PiNativeMethods.ClearMatrix(_matrix);
        }

        public void Fill(RgbColor color)
        {
            PiNativeMethods.FillMatrix(_matrix, color.R, color.G, color.B);
        }

        public void SetPixel(int x, int y, RgbColor color)
        {
            Validate.XY(x, y, Width, Height);

            PiNativeMethods.SetMatrixPixel(_matrix, x, y, color.R, color.G, color.B);
        }

        public void SetCanvas(RgbCanvas canvas)
        {
            if (Width != canvas.Width || Height != canvas.Height)
            {
                throw new ArgumentException("RgbCanvas must be same size as RgbMatrix", nameof(canvas));
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    SetPixel(x, y, canvas.GetPixel(x, y));
                }
            }
        }

        public void SetCanvasAt(int x, int y, RgbCanvas canvas)
        {
            if (!Validate.InRange(x, y, Width, Height))
            {
                return;
            }

            for (int xOff = x; xOff < Math.Min(Width, canvas.Width); xOff++)
            {
                for (int yOff = y; yOff < Math.Min(Height, canvas.Height); yOff++)
                {
                    SetPixel(xOff, yOff, canvas.GetPixel(xOff - x, yOff - y));
                }
            }
        }

        public void SetPartialCanvasAt(int targetX, int targetY, RgbCanvas canvas, int sourceX, int sourceY, int sourceWidth,
            int targetWidth)
        {
            throw new NotImplementedException();
        }

        public void SetPwmBits(byte bits)
        {
            PiNativeMethods.SetMatrixPwmBits(_matrix, bits);
        }

        public void SetWriteCycles(byte writeCycles)
        {
            PiNativeMethods.SetMatrixWriteCycles(_matrix, writeCycles);
        }

        private void ReleaseUnmanagedResources()
        {
            if (_matrix != IntPtr.Zero)
            {
                PiNativeMethods.DeleteMatrix(_matrix);
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~PiRgbMatrix()
        {
            ReleaseUnmanagedResources();
        }
    }
}
