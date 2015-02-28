using System.IO;
using CubeProject.Data.Entities;
using CubeProject.Data.Serializers;
using CubeProject.Infrastructure.Enums;
using NUnit.Framework;

namespace CubeProject.Data.Tests
{
    [TestFixture]
    public class AnimationSerializerTests
    {
        private Animation _animation;

        [SetUp]
        public void Setup()
        {
            _animation = new Animation
            {
                ColorDepth = ColorDepth.Onebit
            };

            var frame = new Frame<PixelColor>(2, 2);
            frame.Data[0, 0] = new PixelColor{Alpha = 254, Red = 120, Green = 120, Blue = 120};
            frame.Data[0, 1] = new PixelColor{Alpha = 254, Red = 120, Green = 120, Blue = 120};
            frame.Data[1, 0] = new PixelColor { Alpha = 254, Red = 120, Green = 120, Blue = 120 };
            frame.Data[1, 1] = new PixelColor { Alpha = 254, Red = 120, Green = 120, Blue = 120 };
            frame.Duration = 500;

            _animation.Frames.Add(frame);

            var frame2 = new Frame<PixelColor>(2, 2);
            frame2.Data[0, 0] = new PixelColor{Alpha = 0, Red = 120, Green = 120, Blue = 120};
            frame2.Data[0, 1] = new PixelColor { Alpha = 0, Red = 120, Green = 120, Blue = 120 };
            frame2.Data[1, 0] = new PixelColor { Alpha = 0, Red = 120, Green = 120, Blue = 120 };
            frame2.Data[1, 1] = new PixelColor { Alpha = 0, Red = 120, Green = 120, Blue = 120 };
            frame2.Duration = 500;

            _animation.Frames.Add(frame2);
        }

        [Test]
        public void TestAnimationSerialization()
        {
            AnimationSerializer serializer = new AnimationSerializer();
            byte[] contentStream = serializer.Serialize(_animation);
            Animation result = serializer.Deserialize(contentStream);

            // pretty poor validation, but it'll do.
            Assert.AreEqual(_animation.Frames.Count, result.Frames.Count);
        }
    }
}
