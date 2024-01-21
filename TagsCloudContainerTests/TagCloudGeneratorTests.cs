using TagsCloudContainer.Interfaces;
using TagsCloudContainer;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudContainer.TagsCloud;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class TagCloudGeneratorTests
    {
        private ITagCloudGenerator tagCloudGenerator;

        [SetUp]
        public void SetUp()
        {
            tagCloudGenerator = new TagCloudGenerator();
        }

        [Test]
        public void GenerateTagCloud_ValidData_ReturnsImage()
        {
            var words = new[] { "apple", "banana", "orange", "apple", "banana" };

            var imageSettings = new ImageSettings();

            var tagCloudImage = tagCloudGenerator.GenerateTagCloud(words, imageSettings);

            tagCloudImage.Should().NotBeNull();
        }
    }
}
