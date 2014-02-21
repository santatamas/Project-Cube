using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CubeProject.Data.Entities;
using CubeProject.Graphics.Helpers;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Graphics.Renderers
{
    public class MatrixRenderer :INotifyPropertyChanged
    {
        #region Construction
        public MatrixRenderer()
        {
            InitializeRenderSource();
        }
        public MatrixRenderer(MatrixInfo settings)
        {
            _settings = settings;
            InitializeRenderSource();
        }

        #endregion

        #region Properties
        public BitmapSource RenderedSource
        {
            get
            {
                _page0.Invalidate();
                return (BitmapSource)_page0;
            }
        }

        public Frame<byte> Frame
        {
            get { return _frame; }
            set
            {
                _frame = value;
                Render();
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitializeRenderSource()
        {
            Format = PixelFormats.Bgra32;
            var section = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, PAGE_READWRITE, 0, Count, null);
            _map = MapViewOfFile(section, FILE_MAP_ALL_ACCESS, 0, 0, Count);
            _page0 = System.Windows.Interop.Imaging.CreateBitmapSourceFromMemorySection(section, Settings.ScreenWidth, Settings.ScreenHeight, Format, Stride, 0) as InteropBitmap;
        }

        public unsafe void Render()
        {
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
                        if (_frame.Data[i, j] == 1 || _frame.Data[i, j] == 255)
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
                        currentPixelColor = Color.FromArgb(_frame.Data[i, j], _pixelOnBrush.R, _pixelOnBrush.G, _pixelOnBrush.B);
                    }
                    UnSafeToolKit.DrawRectange(new Rect(i * rectSize, j * rectSize, Settings.PixelSize, Settings.PixelSize), currentPixelColor, mapPtr, Settings.ScreenWidth);
                }
            }
            #endregion

            OnPropertyChanged("RenderedSource");
        }

        private PixelCoordinate GetCoordinateFromLocation(Point clickLocation)
        {
            return new PixelCoordinate
            {
                X = (int)clickLocation.X / (Settings.PixelSize + Settings.GapSize),
                Y = (int)clickLocation.Y / (Settings.PixelSize + Settings.GapSize)
            };
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

        public MatrixInfo Settings
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

        private Color _backGroundBrush = Color.FromRgb(125, 140, 115);
        private Color _pixelOffBrush = Color.FromRgb(116, 129, 107);
        private Color _pixelOnBrush = Color.FromRgb(53, 53, 53);

        private MatrixInfo _settings;
        private Frame<byte> _frame;

        #endregion
        #endregion

        #region Public

        public void TogglePixelAtLocation(Point clickLocation, int area, ToggleMode mode)
        {
            TogglePixelAtLocation(clickLocation, area, mode, 50);
        }

        public void TogglePixelAtLocation(Point clickLocation, int area, ToggleMode mode, byte shadeLevel)
        {
            if (_frame == null) return;

            PixelCoordinate coordinate = GetCoordinateFromLocation(clickLocation);

            // iterate through the selected pixel, and it's surrounding area
            for (int i = (coordinate.X - area); i <= (coordinate.X + area); i++)
            {
                for (int j = (coordinate.Y - area); j <= (coordinate.Y + area); j++)
                {
                    // checking boundaries
                    if (i >= Settings.SizeX || j >= Settings.SizeY || i < 0 || j < 0) continue;

                    // change pixel's value based on the selected drawing mode
                    switch (mode)
                    {
                        case ToggleMode.On:
                            _frame[i, j] = shadeLevel;
                            break;
                        case ToggleMode.Off:
                            _frame[i, j] = 0;
                            break;
                        case ToggleMode.Inverse:

                            if (_frame[i, j] == 0)
                            {
                                _frame[i, j] = shadeLevel;
                            }
                            else
                            {
                                _frame[i, j] = 0;
                            }
                            
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("mode");
                    }
                }
            }
            Render();
        }
        #endregion
    }
}
