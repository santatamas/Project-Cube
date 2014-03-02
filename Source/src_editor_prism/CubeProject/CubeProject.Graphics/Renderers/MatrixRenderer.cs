using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CubeProject.Graphics.Utilities;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Graphics.Renderers
{
    public class MatrixRenderer
    {
        #region Construction
        public MatrixRenderer()
        {
            InitializeRenderSource();
        }
        public MatrixRenderer(RendererSettings settings)
        {
            _settings = settings;
            InitializeRenderSource();
        }

        #endregion

        #region Private

        private void InitializeRenderSource()
        {
            Format = PixelFormats.Bgra32;
            var section = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, PAGE_READWRITE, 0, Count, null);
            _map = MapViewOfFile(section, FILE_MAP_ALL_ACCESS, 0, 0, Count);
            _page0 = System.Windows.Interop.Imaging.CreateBitmapSourceFromMemorySection(section, Settings.ScreenWidth, Settings.ScreenHeight, Format, Stride, 0) as InteropBitmap;
        }

        #region Private State
        private uint Count
        {
            get
            {
                return (uint)(Settings.ScreenWidth * Settings.ScreenHeight * (Format.BitsPerPixel / 8));
            }
        }

        private int Stride
        {
            get { return (Settings.ScreenWidth * Format.BitsPerPixel / 8); }
        }

        private PixelFormat Format { get; set; }

        public RendererSettings Settings
        {
            get { return _settings; }
        }

        private InteropBitmap _page0;
        private IntPtr _map;

        #region InterOp Calls
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFileMapping(IntPtr hFile,
        IntPtr lpFileMappingAttributes,
        uint flProtect,
        uint dwMaximumSizeHigh,
        uint dwMaximumSizeLow,
        string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject,
        uint dwDesiredAccess,
        uint dwFileOffsetHigh,
        uint dwFileOffsetLow,
        uint dwNumberOfBytesToMap);
        #endregion

        uint FILE_MAP_ALL_ACCESS = 0xF001F;
        uint PAGE_READWRITE = 0x04;
        IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        private Color _pixelOnBrush = Color.FromRgb(53, 53, 53);

        private RendererSettings _settings;

        #endregion
        #endregion

        #region Public
        public unsafe BitmapSource Render(byte[,] frame, int sizeX, int sizeY)
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
                    if (Settings.ColorDepth == ColorDepth.Onebit)
                    {
                        if (frame[i, j] == 1 || frame[i, j] == 255)
                        {
                            currentPixelColor = _pixelOnBrush;
                        }
                        else
                        {
                            currentPixelColor = Color.FromArgb(0, 0, 0, 0);
                        }
                    }
                    else
                    {
                        currentPixelColor = Color.FromArgb(frame[i, j], _pixelOnBrush.R, _pixelOnBrush.G, _pixelOnBrush.B);
                    }
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