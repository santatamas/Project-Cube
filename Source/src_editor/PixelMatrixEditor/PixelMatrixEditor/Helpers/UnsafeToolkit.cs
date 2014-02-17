using System;
using System.Windows;
using System.Windows.Media;

namespace PixelMatrixEditor.Helpers
{
    public static class UnSafeToolKit
    {
        public static unsafe int UnSafeGetPixel(int x, int y, int width, int* imgPtr)
        {
            imgPtr += (y*width) + x;
            return *imgPtr;
        }

        public static unsafe void UnSafeSetPixel(int x, int y, int width, int* imgPtr, int value)
        {
            imgPtr += (y*width) + x;
            *imgPtr = value;
        }

        public static unsafe void DrawRectange(Rect rectangle, Color color, int* imgPtr,int imageWidth)
        {
           int colorCode = (int)GetIntFromColor(color);

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
            return (uint)BitConverter.ToInt32(new byte[] { color.B, color.G, color.R, color.A /*0x00*/ }, 0);
        }
    }
}
