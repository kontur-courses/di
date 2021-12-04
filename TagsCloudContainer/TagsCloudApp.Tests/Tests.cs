using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudApp;

namespace TagsCloud.Tests
{
    public class Tests
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
        }

        [Test]
        public void Main_WithCustomOutputPath_RenderImage()
        {
            Program.Main(new[] {"render", $"-o{customOutputPath}"});
            File.Exists(customOutputPath).Should().BeTrue();
        }

        [Test]
        public void Main_WithWrongsSettings_ThrowsException()
        {
            Program.Main(new[] {"render", "-s -1, -1"});
            File.Exists(defaultOutputPath).Should().BeFalse();
        }

        [Test]
        public void Main_WithCustomSettings_RenderImage()
        {
            Program.Main(new[]
            {
                "render", "--fontFamily", "Consolas", "--maxFont", "64", "--minFont", "10", "--scale", "2",
                "--background", "Black"
            });

            File.Exists(defaultOutputPath).Should().BeTrue();
        }
    }
}