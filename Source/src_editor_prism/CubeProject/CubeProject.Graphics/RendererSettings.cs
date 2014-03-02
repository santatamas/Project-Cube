using CubeProject.Infrastructure.Enums;

namespace CubeProject.Graphics
{
    public struct RendererSettings
    {
        /// <summary>
        /// Horizontal Pixel size of the frame
        /// </summary>
        public int SizeX;

        /// <summary>
        /// Vertical Pixel size of the frame
        /// </summary>
        public int SizeY;

        /// <summary>
        /// Size of a Pixel in screenpixels (typically 8x8)
        /// </summary>
        public int PixelSize;

        /// <summary>
        /// Size of separator between Pixels in screenpixels (typically 2)
        /// </summary>
        public int GapSize;

        /// <summary>
        /// Width of the rendered Bitmap in screenpixels
        /// </summary>
        public int ScreenWidth;

        /// <summary>
        /// Height of the rendered Bitmap in screenpixels
        /// </summary>
        public int ScreenHeight;

        /// <summary>
        /// Represents the rendering depth
        /// </summary>
        public ColorDepth ColorDepth;
    }
}
