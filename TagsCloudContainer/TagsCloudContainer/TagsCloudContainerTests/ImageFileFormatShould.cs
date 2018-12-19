using System;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.TagsCloudContainerTests
{
    [TestFixture]
    internal class ImageFileFormatShould
    {
        private Array _imageFileFormatValues;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _imageFileFormatValues = Enum.GetValues(typeof(ImageFileFormat));
        }

        [Test]
        public void ContainStrictlyTwoFormatTypes()
        {
            _imageFileFormatValues.Length.Should().Be(2);
        }

        [Test]
        public void ContainTypes()
        {
            _imageFileFormatValues.GetValue(0).ToString().Should().Be("Jpg");
            _imageFileFormatValues.GetValue(1).ToString().Should().Be("Png");
        }
    }
}
