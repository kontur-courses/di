using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudApp;

namespace TagsCloud.Tests
{
    public class ProgramTests
    {
        private const string defaultOutputPath = "output.png";
        private const string customOutputPath = "custom_output.png";

        [SetUp]
        public void SetUp()
        {
            foreach (var filePath in new[] {defaultOutputPath, customOutputPath})
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        [Test]
        public void Main_WithDefaultOptions_RenderImage()
        {
            Program.Main(new[] {"render"});
            File.Exists(defaultOutputPath).Should().BeTrue();
        }

        [Test]
        public void Main_WithCustomOutputPath_RenderImage()
        {
            Program.Main(new[] {"render", $"-o{customOutputPath}"});
            File.Exists(customOutputPath).Should().BeTrue();
        }

        [TestCase("--fontFamily", "QWE")]
        [TestCase("--maxFont", "0")]
        [TestCase("--minFont", "0")]
        [TestCase("-s 0, 0")]
        [TestCase("--scale", "0")]
        [TestCase("--background", "Qwe")]
        [TestCase("--colorMapper", "Qwe")]
        [TestCase("--imageFormat", "Qwe")]
        [TestCase("--wordsScale", "Qwe")]
        [TestCase("--ignoreSpeechParts", "AAA")]
        public void Main_WithWrongsSettings_NotRenderImage(params string[] options)
        {
            Program.Main(new[] {"render"}.Concat(options).ToArray());
            File.Exists(defaultOutputPath).Should().BeFalse();
        }

        [TestCase("--fontFamily", "Consolas")]
        [TestCase("--maxFont", "72")]
        [TestCase("--minFont", "1")]
        [TestCase("-s 900, 600")]
        [TestCase("--scale", "5")]
        [TestCase("--background", "255 37 38 40")]
        [TestCase("--colorMapper", "random")]
        [TestCase("--imageFormat", "bmp")]
        [TestCase("--wordsScale", "ln")]
        [TestCase("--ignoreSpeechParts", "COM CONJ INTJ")]
        public void Main_WithCustomSettings_RenderImage(params string[] options)
        {
            Program.Main(new[] {"render"}.Concat(options).ToArray());

            File.Exists(defaultOutputPath).Should().BeTrue();
        }
    }
}