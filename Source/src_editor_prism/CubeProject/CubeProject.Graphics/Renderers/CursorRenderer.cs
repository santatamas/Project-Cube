using System;
using System.Windows;
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
    public class CursorRenderer : RendererBase
    {
        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="CursorRenderer"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// /// <seealso cref="CubeProject.Graphics.RendererSettings"/>
        public CursorRenderer(RendererSettings settings)
            : base(settings)
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
            if (sizeX != Settings.SizeX || sizeY != Settings.SizeY)
                throw new ArgumentException("Renderer called with invalid frame size!");

            #region Pixel render

            Color currentPixelColor = Color.FromArgb(80, _pixelOnBrush.R, _pixelOnBrush.G, _pixelOnBrush.B);
            Color clearColor = Color.FromArgb(0, 0, 0, 0);
            uint* mapPtr = (uint*)_map;
            int rectSize = Settings.PixelSize + Settings.GapSize;

            for (int i = 0; i < Settings.SizeX; i++)
            {
                for (int j = 0; j < Settings.SizeY; j++)
                {
                    UnSafeToolKit.DrawRectange(
                        new Rect(i*rectSize, j*rectSize, Settings.PixelSize, Settings.PixelSize),
                        frame[i, j].Alpha == 0 ? clearColor : currentPixelColor, 
                        mapPtr, 
                        Settings.ScreenWidth);
                }
            }
            #endregion

            _page0.Invalidate();
            return _page0;
        }

        #endregion
    }
}