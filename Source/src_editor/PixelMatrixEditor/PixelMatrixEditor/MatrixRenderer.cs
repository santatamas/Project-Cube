using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PixelMatrixEditor
{
    public class MatrixRenderer :INotifyPropertyChanged
    {
        private InteropBitmap _page0;
        IntPtr _map;

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

        private Color _backGroundBrush = Color.FromRgb(125,140,115);
        private Color _pixelOffBrush = Color.FromRgb(116, 129, 107);
        private Color _pixelOnBrush = Color.FromRgb(53, 53, 53);

        private MatrixInfo _settings;
        private bool[,] _matrixCache;

        public MatrixRenderer()
        {
            InitializeRenderSource();
            Render();
        }

        public MatrixRenderer(MatrixInfo settings)
        {
            _settings = settings;
            _matrixCache = new bool[settings.SizeX,_settings.SizeY];
            InitializeRenderSource();
            Render();
        }

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


        public PixelFormat Format { get; set; }

        public BitmapSource RenderedSource
        {
            get
            {
                _page0.Invalidate();
                return (BitmapSource)_page0;
            }
        }


        private void InitializeRenderSource()
        {
            Format = PixelFormats.Pbgra32;
            var section = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, PAGE_READWRITE, 0, Count, null);
            _map = MapViewOfFile(section, FILE_MAP_ALL_ACCESS, 0, 0, Count);
            _page0 = System.Windows.Interop.Imaging.CreateBitmapSourceFromMemorySection(section, _settings.ScreenWidth, _settings.ScreenHeight, Format, Stride, 0) as InteropBitmap;
        }

        public unsafe void Render()
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
                    currentPixelColor = _matrixCache[i, j] ? _pixelOnBrush : _pixelOffBrush;
                    UnSafeToolKit.DrawRectange(new Rect(i * rectSize, j * rectSize, _settings.PixelSize, _settings.PixelSize), currentPixelColor, mapPtr, _settings.ScreenWidth);
                }
            }
            #endregion
            OnPropertyChanged("RenderedSource");
        }

        internal void TogglePixelAtLocation(Point clickLocation)
        {
            int x = (int)clickLocation.X / (_settings.PixelSize + _settings.GapSize);
            int y = (int)clickLocation.Y / (_settings.PixelSize + _settings.GapSize);

            if (x >= _settings.SizeX || y >= _settings.SizeY) return;

            _matrixCache[x, y] = !_matrixCache[x, y];
            Render();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
