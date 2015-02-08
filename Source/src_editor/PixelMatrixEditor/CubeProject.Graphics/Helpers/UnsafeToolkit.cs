using System;
using System.Windows;
using System.Windows.Media;

namespace CubeProject.Graphics.Helpers
{
    public static class UnSafeToolKit
    {
        public static unsafe uint UnSafeGetPixel(int x, int y, int width, uint* imgPtr)
        {
            imgPtr += (y*width) + x;
            return *imgPtr;
        }

        public static unsafe void UnSafeSetPixel(int x, int y, int width, uint* imgPtr, uint value)
        {
            imgPtr += (y*width) + x;
            *imgPtr = value;
        }

        public static unsafe void DrawRectange(Rect rectangle, Color color, uint* imgPtr,int imageWidth)
        {
           uint colorCode = GetIntFromColor(color);

            for (int i = 0; i < rectangle.Width; i++)
            {
                for (int j = 0; j < rectangle.Height; j++)
                {
                    UnSafeSetPixel((int)rectangle.X + i, (int)rectangle.Y + j, imageWidth, imgPtr, colorCode);
                }
            }
        }

        public static uint GetIntFromColor(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                     (color.G << 8) | (color.B << 0));
        }
    }
}
