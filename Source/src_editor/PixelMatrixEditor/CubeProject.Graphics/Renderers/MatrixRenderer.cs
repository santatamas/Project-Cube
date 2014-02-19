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
using PixelMatrixEditor.Models;

namespace CubeProject.Graphics.Renderers
{
    public class MatrixRenderer :INotifyPropertyChanged
    {
        #region Construction
        public MatrixRenderer()
        {
            InitializeRenderSource();
            Render();
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

        public Frame<byte> Matrix
        {
            get { return _matrix; }
            set
            {
                _matrix = value;
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
            Format = PixelFormats.Pbgra32;
            var section = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, PAGE_READWRITE, 0, Count, null);
            _map = MapViewOfFile(section, FILE_MAP_ALL_ACCESS, 0, 0, Count);
            _page0 = System.Windows.Interop.Imaging.CreateBitmapSourceFromMemorySection(section, _settings.ScreenWidth, _settings.ScreenHeight, Format, Stride, 0) as InteropBitmap;
        }

        private unsafe void Render()
        {
            #region Background render

            uint* buffer = (uint*)_map;

            for (int j = 0; j < _settings.ScreenWidth; j++)
            {
                for (int i = 0; i < _settings.ScreenHeight; i++)
                {
                    *buffer++ = UnSafeToolKit.GetIntFromColor(_backGroundBrush);
                }
            }

            #endregion

            #region Pixel render

            Color currentPixelColor;
            int* mapPtr = (int*)_map;
            int rectSize = _settings.PixelSize + _settings.GapSize;

            for (int i = 0; i < _settings.SizeX; i++)
            {
                for (int j = 0; j < _settings.SizeY; j++)
                {
                    currentPixelColor = _matrix.Data[i, j] == 1 ? _pixelOnBrush : _pixelOffBrush;
                    UnSafeToolKit.DrawRectange(new Rect(i * rectSize, j * rectSize, _settings.PixelSize, _settings.PixelSize), currentPixelColor, mapPtr, _settings.ScreenWidth);
                }
            }
            #endregion

            OnPropertyChanged("RenderedSource");
        }

        private PixelCoordinate GetCoordinateFromLocation(Point clickLocation)
        {
            return new PixelCoordinate
            {
                X = (int)clickLocation.X / (_settings.PixelSize + _settings.GapSize),
                Y = (int)clickLocation.Y / (_settings.PixelSize + _settings.GapSize)
            };
        }

        #region Private State
        private uint Count
        {
            get
            {
                return (uint)(_settings.ScreenWidth * _settings.ScreenHeight * (Format.BitsPerPixel / 8));
            }
        }

        private int Stride
        {
            get { return (_settings.ScreenWidth * Format.BitsPerPixel / 8); }
        }

        private PixelFormat Format { get; set; }

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
        private Frame<byte> _matrix;

        #endregion
        #endregion

        #region Public
        public void TogglePixelAtLocation(Point clickLocation, int area, ToggleMode mode)
        {
            if (_matrix == null) return;

            PixelCoordinate coordinate = GetCoordinateFromLocation(clickLocation);

            // iterate through the selected pixel, and it's surrounding area
            for (int i = (coordinate.X - area); i <= (coordinate.X + area); i++)
            {
                for (int j = (coordinate.Y - area); j <= (coordinate.Y + area); j++)
                {
                    // checking boundaries
                    if (i >= _settings.SizeX || j >= _settings.SizeY || i < 0 || j < 0) continue;

                    // change pixel's value based on the selected drawing mode
                    switch (mode)
                    {
                        case ToggleMode.On:
                            _matrix[i, j] = 1;
                            break;
                        case ToggleMode.Off:
                            _matrix[i, j] = 0;
                            break;
                        case ToggleMode.Inverse:

                            if (_matrix[i, j] == 0)
                            {
                                _matrix[i, j] = 1;
                            }
                            else
                            {
                                _matrix[i, j] = 0;
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
