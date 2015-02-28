using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CubeProject.Data.Entities;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Data.Converters
{
    public static class GifConverter
    {
        public static Animation Convert(byte[] fileData)
        {
            //try
            //{
                MemoryStream bitmapStream = new MemoryStream(fileData);
                GifBitmapDecoder gifDecoder = new GifBitmapDecoder(bitmapStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                Animation result = new Animation();
                result.ColorDepth = ColorDepth.GrayScale;

                // assume that first frame has the same dimension as the others
                short frameWidth = (short)gifDecoder.Frames[0].PixelWidth;
                short frameHeight = (short)gifDecoder.Frames[0].PixelHeight;

                BitmapSource source = new RenderTargetBitmap(frameWidth, frameHeight, gifDecoder.Frames[0].DpiX, gifDecoder.Frames[0].DpiY, PixelFormats.Pbgra32);
                BitmapSource prevFrame = null;
                FrameInfo prevInfo = null;
                foreach (var rawFrame in gifDecoder.Frames)
                {
                    var info = GetFrameInfo(rawFrame);
                    var frame = MakeFrame(source, rawFrame, info, prevFrame, prevInfo);

                    var animFrame = new Frame<PixelColor>((short)frame.PixelWidth, (short)frame.PixelHeight);
                    var pixels = GetPixels(frame);

                    animFrame.Duration = (short)info.Delay.TotalMilliseconds;
                    animFrame.ColorDepth = ColorDepth.GrayScale;
                    prevFrame = frame;
                    prevInfo = info;

                    for (int i = 0; i < frameWidth; i++)
                    {
                        for (int j = 0; j < frameHeight; j++)
                        {
                            animFrame[i, j] = pixels[i, j];
                        }
                    }
                    result.Frames.Add(animFrame);
                }
                return result;
            //}
            //catch (Exception ex)
            //{
            //    throw new InvalidOperationException("Error during gif conversion.", ex);
            //}
        }

        private static PixelColor[,] GetPixels(BitmapSource source)
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

        private static BitmapSource MakeFrame(BitmapSource fullImage, BitmapSource rawFrame, FrameInfo frameInfo, BitmapSource previousFrame, FrameInfo previousFrameInfo)
        {
            DrawingVisual visual = new DrawingVisual();
            using (var context = visual.RenderOpen())
            {
                if (previousFrameInfo != null && previousFrame != null &&
                    previousFrameInfo.DisposalMethod == FrameDisposalMethod.Combine)
                {
                    var fullRect = new Rect(0, 0, fullImage.PixelWidth, fullImage.PixelHeight);
                    context.DrawImage(previousFrame, fullRect);
                }

                context.DrawImage(rawFrame, frameInfo.Rect);
            }
            var bitmap = new RenderTargetBitmap(
                fullImage.PixelWidth, fullImage.PixelHeight,
                fullImage.DpiX, fullImage.DpiY,
                PixelFormats.Pbgra32);
            bitmap.Render(visual);
            return bitmap;
        }

        private class FrameInfo
        {
            public TimeSpan Delay { get; set; }
            public FrameDisposalMethod DisposalMethod { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public double Left { get; set; }
            public double Top { get; set; }

            public Rect Rect
            {
                get { return new Rect(Left, Top, Width, Height); }
            }
        }

        private enum FrameDisposalMethod
        {
            Replace = 0,
            Combine = 1,
            RestoreBackground = 2,
            RestorePrevious = 3
        }

        private static FrameInfo GetFrameInfo(BitmapFrame frame)
        {
            var frameInfo = new FrameInfo
            {
                Delay = TimeSpan.FromMilliseconds(100),
                DisposalMethod = FrameDisposalMethod.Replace,
                Width = frame.PixelWidth,
                Height = frame.PixelHeight,
                Left = 0,
                Top = 0
            };

            BitmapMetadata metadata;
            try
            {
                metadata = frame.Metadata as BitmapMetadata;
                if (metadata != null)
                {
                    const string delayQuery = "/grctlext/Delay";
                    const string disposalQuery = "/grctlext/Disposal";
                    const string widthQuery = "/imgdesc/Width";
                    const string heightQuery = "/imgdesc/Height";
                    const string leftQuery = "/imgdesc/Left";
                    const string topQuery = "/imgdesc/Top";

                    var delay = metadata.GetQueryOrNull<ushort>(delayQuery);
                    if (delay.HasValue)
                        frameInfo.Delay = TimeSpan.FromMilliseconds(10 * delay.Value);

                    var disposal = metadata.GetQueryOrNull<byte>(disposalQuery);
                    if (disposal.HasValue)
                        frameInfo.DisposalMethod = (FrameDisposalMethod)disposal.Value;

                    var width = metadata.GetQueryOrNull<ushort>(widthQuery);
                    if (width.HasValue)
                        frameInfo.Width = width.Value;

                    var height = metadata.GetQueryOrNull<ushort>(heightQuery);
                    if (height.HasValue)
                        frameInfo.Height = height.Value;

                    var left = metadata.GetQueryOrNull<ushort>(leftQuery);
                    if (left.HasValue)
                        frameInfo.Left = left.Value;

                    var top = metadata.GetQueryOrNull<ushort>(topQuery);
                    if (top.HasValue)
                        frameInfo.Top = top.Value;
                }
            }
            catch (NotSupportedException)
            {
            }

            return frameInfo;
        }

        private static T? GetQueryOrNull<T>(this BitmapMetadata metadata, string query)
        where T : struct
        {
            if (metadata.ContainsQuery(query))
            {
                object value = metadata.GetQuery(query);
                if (value != null)
                    return (T)value;
            }
            return null;
        }
    }
}
