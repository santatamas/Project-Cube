using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CubeProject.Data.Entities;

namespace CubeProject.Data.Converters
{
    public class GifConverter
    {
        public Animation Convert(byte[] fileData)
        {
            try
            {
                MemoryStream bitmapStream = new MemoryStream(fileData);
                GifBitmapDecoder gifDecoder = new GifBitmapDecoder(bitmapStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                Animation result = new Animation();

                foreach (var frame in gifDecoder.Frames)
                {
                    var animFrame = new Frame<byte>((short)frame.PixelWidth, (short)frame.PixelHeight);
                    var pixels = GetPixels(frame);

                    for (int i = 0; i < frame.PixelHeight; i++)
                    {
                        for (int j = 0; j < frame.PixelWidth; j++)
                        {
                            animFrame[i, j] = GetFrameValueByColor(pixels[i, j]);
                        }
                    }
                    result.Frames.Add(animFrame);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error during gif conversion.",ex);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PixelColor
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        public PixelColor[,] GetPixels(BitmapSource source)
        {
            if (source.Format != PixelFormats.Bgra32)
                source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);

            int width = source.PixelWidth;
            int height = source.PixelHeight;
            PixelColor[,] result = new PixelColor[width, height];

            var pixelBytes = new byte[height * width * 4];
            source.CopyPixels(pixelBytes, width * 4, 0);
            int y0 = 0 / width;
            int x0 = 0 - width * y0;
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    result[x + x0, y + y0] = new PixelColor
                    {
                        Blue = pixelBytes[(y * width + x) * 4 + 0],
                        Green = pixelBytes[(y * width + x) * 4 + 1],
                        Red = pixelBytes[(y * width + x) * 4 + 2],
                        Alpha = pixelBytes[(y * width + x) * 4 + 3],
                    };

            return result;
        }

        private static byte GetFrameValueByColor(PixelColor colorBytes)
        {
            // Standard green background
            if (colorBytes.Red == 125 &&
                colorBytes.Green == 140 &&
                colorBytes.Blue == 115)
            {
                return 0;
            }

            // Shade Level 1
            if (colorBytes.Red == 110 &&
                colorBytes.Green == 123 &&
                colorBytes.Blue == 102)
            {
                return 50;
            }

            // Shade Level 2
            if (colorBytes.Red == 97 &&
                colorBytes.Green == 106 &&
                colorBytes.Blue == 91)
            {
                return 100;
            }

            // Shade Level 3
            if (colorBytes.Red == 82 &&
                colorBytes.Green == 89 &&
                colorBytes.Blue == 78)
            {
                return 150;
            }

            // Shade Level 4
            if (colorBytes.Red == 69 &&
                colorBytes.Green == 72 &&
                colorBytes.Blue == 67)
            {
                return 200;
            }

            // Shade Level 5
            if (colorBytes.Red == 53 &&
                colorBytes.Green == 53 &&
                colorBytes.Blue == 53)
            {
                return 255;
            }

            // Shade Level 5
            if (colorBytes.Red == 255 &&
                colorBytes.Green == 255 &&
                colorBytes.Blue == 255)
            {
                return 255;
            }

            return 255;
        }
    }
}
