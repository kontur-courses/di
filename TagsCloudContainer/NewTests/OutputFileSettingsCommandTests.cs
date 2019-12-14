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
    class OutputFileSettingsCommandTests
    {
        private readonly ISettingsCommand command = new OutputFileSettingsCommand();
        private IInitialSettings settings;

        [SetUp]
        public void CreateSettings()
        {
            settings = new InitialSettings();
        }

        [Test]
        public void TryChangeSettings_ShouldReturnResultWithError_IfArgumentsCountLessThanTwo()
        {
            var arguments = new[] { "output" };

            var result = command.TryChangeSettings(arguments, settings);
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void TryChangeSettings_ShouldReturnOkResult_IfCorrectArguments()
        {
            var arguments = new[] { "output", "image.png" };

            var result = command.TryChangeSettings(arguments, settings);
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void TryChangeSettings_ShouldChangeSettings_IfResultIsOk()
        {
            var arguments = new[] { "output", "image.png" };

            var result = command.TryChangeSettings(arguments, settings);
            result.Value.OutputFilePath.Should().Be("image.png");
        }
    }
}
