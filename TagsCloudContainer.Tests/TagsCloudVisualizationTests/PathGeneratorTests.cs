using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudVisualization.Tests.TagsCloudVisualizationTests
{
    public class PathGeneratorTests
    {
        private PathGenerator Sut { get; set; }

        [SetUp]
        public void SetUp()
        {
            Sut = new PathGenerator(new DateTimeProvider());
        }

        [Test]
        public void GetNewFilePath_ReturnExistingDirectory_WhenCalled()
        {
            var path = Sut.GetNewFilePath().Split('\\');
            path[^1] = "";
            var directoryPath = string.Join('\\', path);

            Directory.Exists(directoryPath).Should().BeTrue();
        }
    }
}