using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.UI;
using TagsCloudContainer.UI.SettingsCommands;

namespace TagsCloudContainer.NewTests
{
    class ImageSizeSettingsCommandTests
    {
        private readonly ISettingsCommand command = new ImageSizeSettingsCommand();
        private IInitialSettings settings;

        [SetUp]
        public void CreateSettings()
        {
            settings = new InitialSettings();
        }

        [Test]
        public void TryChangeSettings_ShouldReturnResultWithError_IfArgumentsCountLessThanThree()
        {
            var arguments = new [] {"size", "1000"};

            var result = command.TryChangeSettings(arguments, settings);
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void TryChangeSettings_ShouldReturnResultWithError_IfArgumentsNotIntegers()
        {
            var arguments = new[] { "size", "10a", "10b" };

            var result = command.TryChangeSettings(arguments, settings);
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void TryChangeSettings_ShouldReturnResultWithError_IfArgumentsAreNegative()
        {
            var arguments = new[] { "size", "1000", "-1000" };

            var result = command.TryChangeSettings(arguments, settings);
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void TryChangeSettings_ShouldReturnOkResult_IfCorrectArguments()
        {
            var arguments = new[] { "size", "1000", "1000" };

            var result = command.TryChangeSettings(arguments, settings);
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void TryChangeSettings_ShouldChangeSettings_IfResultIsOk()
        {
            var arguments = new[] { "size", "1000", "1000" };

            var result = command.TryChangeSettings(arguments, settings);
            result.Value.ImageSize.Should().Be(new Size(1000, 1000));
        }
    }
}
