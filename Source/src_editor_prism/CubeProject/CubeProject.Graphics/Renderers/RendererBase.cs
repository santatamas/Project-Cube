using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CubeProject.Data.Entities;

namespace CubeProject.Graphics.Renderers
{
    /// <summary>
    /// Provides in-memory bitmap render capability for byte multiarrays.
    /// Before use, please provide a preconfigured <see cref="CubeProject.Graphics.RendererSettings"/> object.
    /// </summary>
    /// <seealso cref="CubeProject.Graphics.RendererSettings"/>
    public abstract class RendererBase : IDisposable
    {
        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="{"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// /// <seealso cref="CubeProject.Graphics.RendererSettings"/>
        protected RendererBase(RendererSettings settings)
        {
            _settings = settings;
            InitializeRenderSource();
        }

        #endregion

        #region Private

        private void InitializeRenderSource()
        {
            Format = PixelFormats.Bgra32;
            _section = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, PAGE_READWRITE, 0, Count, null);
            _map = MapViewOfFile(_section, FILE_MAP_ALL_ACCESS, 0, 0, Count);
            _page0 = System.Windows.Interop.Imaging.CreateBitmapSourceFromMemorySection(_section, Settings.ScreenWidth, Settings.ScreenHeight, Format, Stride, 0) as InteropBitmap;
        }

        #region Private State
        protected uint Count
        {
            get
            {
                return (uint)(Settings.ScreenWidth * Settings.ScreenHeight * (Format.BitsPerPixel / 8));
            }
        }

        protected int Stride
        {
            get { return (Settings.ScreenWidth * Format.BitsPerPixel / 8); }
        }

        protected PixelFormat Format { get; set; }


        /// <summary>
        /// Gets the RendererSettings.
        /// </summary>
        /// <value>
        /// The RendererSettings.
        /// </value>
        public RendererSettings Settings
        {
            get { return _settings; }
        }

        protected InteropBitmap _page0;
        protected IntPtr _map;
        protected IntPtr _section;

        #region InterOp Calls
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool UnmapViewOfFile(IntPtr hMap);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hHandle);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

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

        protected Color _pixelOnBrush = Color.FromRgb(53, 53, 53);

        protected RendererSettings _settings;
        private bool disposed;
        private object _lockObject = new object();

        #endregion
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
        public abstract unsafe BitmapSource Render(PixelColor[,] frame, int sizeX, int sizeY);

        #endregion

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (LockObject)
            {
                if (disposing)
                {
                    // free managed resources
                }
                // free native resources if there are any.
                if (_map != IntPtr.Zero)
                {
                    UnmapViewOfFile(_map);
                    _map = IntPtr.Zero;
                }
                if (_section != IntPtr.Zero)
                {
                    CloseHandle(_section);
                    _section = IntPtr.Zero;
                }
            }
        }

        private object LockObject
        {
            get { return _lockObject; }
            set { _lockObject = value; }
        }
    }
}