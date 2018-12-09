using System;
using System.Collections.Generic;
using System.Text;

namespace piled
{
    public interface IRgbMatrix : IDisposable
    {
        int Width { get; }
        int Height { get; }

        void Clear();

        void Fill(RgbColor color);

        void SetPixel(int x, int y, RgbColor color);
        
        /// <summary>
        /// Sets the canvas into the RgbMatrix.
        /// The RgbCanvas must be the same width/height as the RgbMatrix or an ArgumentException will be thrown.
        /// </summary>
        /// <param name="canvas"></param>
        void SetCanvas(RgbCanvas canvas);

        /// <summary>
        /// Sets an RgbCanvas at an x,y offset.  This operation will clip canvas as needed.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="canvas"></param>
        void SetCanvasAt(int x, int y, RgbCanvas canvas);

        /// <summary>
        /// Sets an RgbCanvas at targetX, targetY offset.
        /// Only the part of the canvas specified by (sourceX, sourceY) of dimensions (sourceWidth, targetWidth) will be set.
        /// This operation will clip the canvas image as needed.
        /// </summary>
        /// <param name="targetX"></param>
        /// <param name="targetY"></param>
        /// <param name="canvas"></param>
        /// <param name="sourceX"></param>
        /// <param name="sourceY"></param>
        /// <param name="sourceWidth"></param>
        /// <param name="targetWidth"></param>
        void SetPartialCanvasAt(
            int targetX, 
            int targetY, 
            RgbCanvas canvas, 
            int sourceX, 
            int sourceY, 
            int sourceWidth,
            int targetWidth);

        void SetPwmBits(byte bits);
        void SetWriteCycles(byte writeCycles);
    }
}
