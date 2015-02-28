using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CubeProject.Data.Entities;
using CubeProject.Graphics.Utilities;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Graphics.Renderers
{
    /// <summary>
    /// Provides in-memory bitmap render capability for byte multiarrays.
    /// Before use, please provide a preconfigured <see cref="CubeProject.Graphics.RendererSettings"/> object.
    /// </summary>
    /// <seealso cref="CubeProject.Graphics.RendererSettings"/>
    public class MatrixRenderer : RendererBase
    {
        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixRenderer"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// /// <seealso cref="CubeProject.Graphics.RendererSettings"/>
        public MatrixRenderer(RendererSettings settings) : base(settings)
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

            #region Background render

            //uint* buffer = (uint*)_map;

            //for (int j = 0; j < Settings.ScreenWidth; j++)
            //{
            //    for (int i = 0; i < Settings.ScreenHeight; i++)
            //    {
            //        *buffer++ = UnSafeToolKit.GetIntFromColor(_backGroundBrush);
            //    }
            //}

            #endregion

            #region Pixel render

            Color currentPixelColor;
            uint* mapPtr = (uint*)_map;
            int rectSize = Settings.PixelSize + Settings.GapSize;

            for (int i = 0; i < Settings.SizeX; i++)
            {
                for (int j = 0; j < Settings.SizeY; j++)
                {
                    currentPixelColor = Color.FromArgb(frame[i, j].Alpha, frame[i, j].Red, frame[i, j].Green, frame[i, j].Blue);
                    UnSafeToolKit.DrawRectange(new Rect(i * rectSize, j * rectSize, Settings.PixelSize, Settings.PixelSize), currentPixelColor, mapPtr, Settings.ScreenWidth);
                }
            }
            #endregion

            _page0.Invalidate();
            return _page0;
        }

        #endregion
    }
}