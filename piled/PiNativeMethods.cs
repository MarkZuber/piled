using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace piled
{
    internal static class PiNativeMethods
    {
        private const string PiDllName = "rpi-rgb-led-matrix";

        [DllImport(PiDllName)]
        public static extern IntPtr CreateMatrix(int height, int cells);

        [DllImport(PiDllName)]
        public static extern void DeleteMatrix(IntPtr matrix);

        [DllImport(PiDllName)]
        public static extern void ClearMatrix(IntPtr matrix);

        [DllImport(PiDllName)]
        public static extern void FillMatrix(IntPtr matrix, byte r, byte g, byte b);

        [DllImport(PiDllName)]
        public static extern void SetMatrixPixel(IntPtr matrix, int x, int y, byte r, byte g, byte b);

        [DllImport(PiDllName)]
        public static extern void SetMatrixPwmBits(IntPtr matrix, byte pwmBits);

        [DllImport(PiDllName)]
        public static extern void SetMatrixWriteCycles(IntPtr matrix, byte writeCycles);
    }
}
