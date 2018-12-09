/*------------------------------------------------------------------------
  Straight C API control Adafruit RGB Matrix HAT for Raspberry Pi from .NET Core

  Very very simple at the moment...just a wrapper around the SetPixel,
  Fill, Clear and SetPWMBits functions of the librgbmatrix library.
  Graphics primitives can be handled using the piled .NET library also in this project.
  ------------------------------------------------------------------------*/

#include "led-matrix.h"

using rgb_matrix::GPIO;
using rgb_matrix::RGBMatrix;

static GPIO io;

// #define EXPORT __attribute__((visibility("default")))
#define DLL_PUBLIC __attribute__ ((visibility ("default")))

extern "C" DLL_PUBLIC void* CreateMatrix(uint32_t height, uint32_t cells) {
    io.Init();
    RGBMatrix* matrix = new RGBMatrix(&io, height, cells);
    matrix->Clear();
    return matrix;
}

extern "C" DLL_PUBLIC void DeleteMatrix(void* matrix) {
    RGBMatrix* mat = (RGBMatrix*)matrix;
    delete mat;
    mat = nullptr;
}

extern "C" DLL_PUBLIC void ClearMatrix(void* matrix) {
    RGBMatrix* mat = (RGBMatrix*)matrix;
    mat->Clear();
}

extern "C" DLL_PUBLIC void FillMatrix(void* matrix, uint8_t r, uint8_t g, uint8_t b) {
    RGBMatrix* mat = (RGBMatrix*)matrix;
    mat->Fill(r, g, b);
}

extern "C" DLL_PUBLIC void SetMatrixPixel(void* matrix, uint32_t x, uint32_t y, uint8_t r, uint8_t g, uint8_t b) {
    RGBMatrix* mat = (RGBMatrix*)matrix;
    mat->SetPixel(x, y, r, g, b);
}

extern "C" DLL_PUBLIC void SetMatrixPwmBits(void* matrix, uint8_t bits) {
    RGBMatrix* mat = (RGBMatrix*)matrix;
    mat->SetPWMBits(bits);
}

extern "C" DLL_PUBLIC void SetMatrixWriteCycles(void* matrix, uint8_t writeCycles) {
    io.writeCycles = writeCycles;
}
