using System;

static internal class Validate
{
    public static void Range(int value, int minValue, int maxValue)
    {
        if (value < minValue || value > maxValue)
        {
            throw new ArgumentException($"{nameof(value)} ({value}) must be in the range [{minValue}, {maxValue}]");
        }
    }

    public static bool InRange(int x, int y, int width, int height)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public static void XY(int x, int y, int width, int height)
    {
        Validate.Range(x, 0, width-1);
        Validate.Range(y, 0, height-1);
    }
}