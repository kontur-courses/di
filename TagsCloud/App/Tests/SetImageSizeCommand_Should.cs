using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App.Commands;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Tests
{
    public class SetImageSizeCommand_Should
    {
        private SetImageSizeCommand command;
        private IImageSizeProvider imageSizeProvider;

        [SetUp]
        public void SetUp()
        {
            imageSizeProvider = A.Fake<IImageSizeProvider>();
            command = new SetImageSizeCommand(imageSizeProvider);
        }

        [Test]
        public void Execute_Set_ImageSize()
        {
            var newImageSize = new ImageSize
            {
                Height = 150,
                Width = 150
            };
            command.Execute(new[] {"150", "150"});
            imageSizeProvider.ImageSize.Height.Should().Be(newImageSize.Height);
            imageSizeProvider.ImageSize.Width.Should().Be(newImageSize.Width);
        }
    }
}