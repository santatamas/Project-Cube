using System.Windows;
using System.Windows.Media;

namespace CubeProject.Graphics.Utilities
{
    /// <summary>
    /// Provides static methods for easy in-memory bitmap operations.
    /// </summary>
    public static class UnSafeToolKit
    {
        /// <summary>
        /// Gets the int color value of the specified pixel from the in-memory bitmap.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="width">The width of the in-memory bitmap.</param>
        /// <param name="imgPtr">Pointer for the in-memory bitmap.</param>
        /// <returns></returns>
        public static unsafe uint UnSafeGetPixel(int x, int y, int width, uint* imgPtr)
        {
            imgPtr += (y*width) + x;
            return *imgPtr;
        }

        /// <summary>
        /// Sets the int color value of the specified pixel from the in-memory bitmap.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="width">The width of the in-memory bitmap.</param>
        /// <param name="imgPtr">Pointer for the in-memory bitmap.</param>
        /// <param name="value">The color value in 32bit ARGB format.</param>
        public static unsafe void UnSafeSetPixel(int x, int y, int width, uint* imgPtr, uint value)
        {
            imgPtr += (y*width) + x;
            *imgPtr = value;
        }

        /// <summary>
        /// Draws a rectange on the provided in-memory bitmap.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="color">The color.</param>
        /// <param name="imgPtr">Pointer for the in-memory bitmap.</param>
        /// <param name="imageWidth">The width of the in-memory bitmap.</param>
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

        /// <summary>
        /// Gets the 32-bit ARGB color value from the provided <see cref="System.Windows.Media.Color"/>. 
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The 32-bit ARGB color value.</returns>
        public static uint GetIntFromColor(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                     (color.G << 8) | (color.B << 0));
        }
    }
}
