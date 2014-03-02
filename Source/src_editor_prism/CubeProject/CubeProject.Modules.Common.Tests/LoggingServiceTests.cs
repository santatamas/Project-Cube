using System.Diagnostics;
using System.IO;
using System.Text;
using CubeProject.Modules.Common.Services;
using NUnit.Framework;

namespace CubeProject.Modules.Common.Tests
{
    [TestFixture]
    public class LoggingServiceTests
    {
        readonly StringBuilder _stringBuilder = new StringBuilder();
        [TestFixtureSetUp]
        public void Setup()
        {
            TextWriter tw = new StringWriter(_stringBuilder);
            Debug.Listeners.AddRange(new TraceListener[]{new TextWriterTraceListener(tw, "stringWriterListener")});
        }

        [SetUp]
        public void SetUp()
        {
            _stringBuilder.Clear();
        }

        [Test]
        public void TestLogInfo()
        {
            LoggingService ls = new LoggingService();
            ls.LogInfo("logInfoMessage");

            Assert.IsTrue(_stringBuilder.ToString().Contains("[Info]") && _stringBuilder.ToString().Contains("logInfoMessage"));
        }

        [Test]
        public void TestLogError()
        {
            LoggingService ls = new LoggingService();
            ls.LogError("logInfoMessage");

            Assert.IsTrue(_stringBuilder.ToString().Contains("[Error]") && _stringBuilder.ToString().Contains("logInfoMessage"));
        }

        [Test]
        public void TestLogWarning()
        {
            LoggingService ls = new LoggingService();
            ls.LogWarning("logInfoMessage");

            Assert.IsTrue(_stringBuilder.ToString().Contains("[Warning]") && _stringBuilder.ToString().Contains("logInfoMessage"));
        }
    }
}
