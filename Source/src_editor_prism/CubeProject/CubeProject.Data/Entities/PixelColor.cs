using System.Runtime.InteropServices;

namespace CubeProject.Data.Entities
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PixelColor
    {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
    }
}
