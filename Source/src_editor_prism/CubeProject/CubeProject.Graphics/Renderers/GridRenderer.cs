using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CubeProject.Data.Entities;
using CubeProject.Graphics.Utilities;

namespace CubeProject.Graphics.Renderers
{
    /// <summary>
    /// Provides in-memory bitmap render capability for byte multiarrays.
    /// Before use, please provide a preconfigured <see cref="CubeProject.Graphics.RendererSettings"/> object.
    /// </summary>
    /// <seealso cref="CubeProject.Graphics.RendererSettings"/>
    public class GridRenderer : RendererBase
    {
        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="GridRenderer"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// /// <seealso cref="CubeProject.Graphics.RendererSettings"/>
        public GridRenderer(RendererSettings settings) : base(settings)
        {
        }

        #endregion

        #region Public
        /// <summary>
        /// Renders the specified frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <param name="sizeX">The size x.</param>
        /// <param name="sizeY">The size y.</param>
        /// <returns>A memory-mapped BitmapSource</returns>
        /// <exception cref="System.ArgumentException">Renderer called with invalid frame size!</exception>
        public override unsafe BitmapSource Render(PixelColor[,] frame, int sizeX, int sizeY)
        {

            #region Helper Grid

            uint* mapPtr = (uint*)_map;
            int rectSize = Settings.PixelSize + Settings.GapSize;
            uint gridColor = UnSafeToolKit.GetIntFromColor(Color.FromArgb(50, 0, 0, 0));
            // vertical lines
            for (int i = 1; i < Settings.SizeX + 1; i++)
            {
                for (int y = 0; y < Settings.ScreenHeight; y++)
                {
                    for (int gapsize = -2; gapsize < 0; gapsize++)
                    {
                        UnSafeToolKit.UnSafeSetPixel((i * rectSize) + gapsize, y, Settings.ScreenWidth, mapPtr, gridColor);
                    }
                }
            }
            //horizontal lines
            for (int j = 1; j < Settings.SizeY + 1; j++)
            {
                for (int x = 0; x < Settings.ScreenWidth; x++)
                {
                    for (int gapsize = -2; gapsize < 0; gapsize++)
                    {
                        UnSafeToolKit.UnSafeSetPixel(x, (j * rectSize) + gapsize, Settings.ScreenWidth, mapPtr, gridColor);
                    }
                }
            }

            #endregion

            _page0.Invalidate();
            return _page0;
        }

        #endregion
    }
}