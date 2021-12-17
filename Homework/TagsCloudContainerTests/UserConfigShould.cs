using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using TagsCloudContainer.Client;

namespace CloudContainerTests
{
    [TestFixture]
    public class UserConfigShould
    {
        private string inputFile = "input.txt";

        [SetUp]
        public void CreateInputFile()
        {
            using (var fs = File.Create(inputFile)) { }
        }

        [TearDown]
        public void DeleteInputFile()
        {
            File.Delete(inputFile);
        }

        [TestCaseSource(nameof(IncorrectOptions))]
        public void Throw_ArgumentException_When(Options incorrectOptions)
        {
            FluentActions.Invoking(() => new UserConfig().GetConfig(incorrectOptions))
                .Should().Throw<ArgumentException>();
        }

        private static IEnumerable<TestCaseData> IncorrectOptions()
        {
            var unknownImageFormat = GetDefaultOptions();
            unknownImageFormat.OutputFileFormat = "hz";
            yield return new TestCaseData(unknownImageFormat)
                .SetName("output file format is unknown");

            var unknownTextFormat = GetDefaultOptions();
            unknownTextFormat.InputFileFormat = "hz";
            yield return new TestCaseData(unknownTextFormat)
                .SetName("input file format is unknown");

            var unknownFontName = GetDefaultOptions();
            unknownFontName.FontName = "hz";
            yield return new TestCaseData(unknownFontName)
                .SetName("font name is unknown");

            var incorrectFontSize = GetDefaultOptions();
            incorrectFontSize.FontSize = -1;
            yield return new TestCaseData(incorrectFontSize)
                .SetName("font size is less than zero");

            var incorrectImageSize = GetDefaultOptions();
            incorrectImageSize.Width = -50;
            yield return new TestCaseData(incorrectImageSize)
                .SetName("image width is less than zero");

            var incorrectImageHeight = GetDefaultOptions();
            incorrectImageHeight.Height = 0;
            yield return new TestCaseData(incorrectImageHeight)
                .SetName("image height equals zero");

            var unknownColorScheme = GetDefaultOptions();
            unknownColorScheme.Color = 5;
            yield return new TestCaseData(unknownColorScheme)
                .SetName("color scheme is unknown");

            var unknownSpiralType = GetDefaultOptions();
            unknownSpiralType.Spiral = "hz";
            yield return new TestCaseData(unknownSpiralType)
                .SetName("spiral type is unknown");
        }

        private static Options GetDefaultOptions()
        {
            return new Options()
            {
                Input = "input.txt",
                Output = "output",
                Height = 1000,
                Width = 1000,
                FontName = "Arial",
                FontSize = 20,
                Color = 0,
                InputFileFormat = "txt",
                OutputFileFormat = "png",
                Spiral = "log",
                ExcludedWords = new[] { "" }
            };
        }
    }


}
