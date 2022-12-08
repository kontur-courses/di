using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    internal class MyConigValidatorTests
    {
        private MyConfiguration config;

        [SetUp]
        public void Setup()
        {
            config = new MyConfiguration
            {
                TextsPath = "c:\\Windows\\System32",
                WordsFileName = "cmd.exe",
                BoringWordsName = "cmd.exe",
                Font = "Arial",
                PictureSize = 600,
                FontSize = 15,
                BackgroundColor = "White",
                FontColor = "Blue"
            };
        }

        [Test]
        public void ValidateConfig_AddPreSetConfig_ShouldNotThrowAnyExceptions()
        {
            var act = () => MyConfigValidator.ValidateConfig(config);

            act.Should().NotThrow();
        }

        [Test]
        public void ValidateConfig_AddPreSetConfigWithLowerCaseColor_ShouldNotThrowAnyExceptions()
        {
            config.FontColor = "white";

            var act = () => MyConfigValidator.ValidateConfig(config);

            act.Should().NotThrow();
        }

        [Test]
        public void ValidateConfig_AddConfigWithEmptyTextsPath_ShouldThrowArgumentException()
        {
            config.TextsPath = "";

            var act = () => MyConfigValidator.ValidateConfig(config);

            act.Should().Throw<ArgumentException>().WithMessage("Texts directory does not exist");
        }

        [Test]
        public void ValidateConfig_AddConfigWithEmptyWordsFileName_ShouldThrowArgumentException()
        {
            config.WordsFileName = "";

            var act = () => MyConfigValidator.ValidateConfig(config);

            act.Should().Throw<ArgumentException>().WithMessage("Tag file does not exist");
        }

        [Test]
        public void ValidateConfig_AddConfigWithEmptyBoringWordsFileName_ShouldThrowArgumentException()
        {
            config.BoringWordsName = "";

            var act = () => MyConfigValidator.ValidateConfig(config);

            act.Should().Throw<ArgumentException>().WithMessage("Exclude words file does not exist");
        }

        [TestCase("")]
        [TestCase("NonExistingFont")]
        public void ValidateConfig_AddConfigWithIncorectFontName_ShouldThrowArgumentException(string font)
        {
            config.Font = font;

            var act = () => MyConfigValidator.ValidateConfig(config);

            act.Should().Throw<ArgumentException>().WithMessage($"Font \"{font}\" can't be found");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateConfig_AddConfigWithPictureSizeLessThanOne_ShouldThrowArgumentException(int size)
        {
            config.PictureSize = size;

            var act = () => MyConfigValidator.ValidateConfig(config);

            act.Should().Throw<ArgumentException>().WithMessage("Picture size should be above 0");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateConfig_AddConfigWithFontSizeLessThanOne_ShouldThrowArgumentException(int size)
        {
            config.FontSize = size;

            var act = () => MyConfigValidator.ValidateConfig(config);

            act.Should().Throw<ArgumentException>().WithMessage("Font size should be above 0");
        }

        [TestCase("")]
        [TestCase("NonExistingColor")]
        public void ValidateConfig_AddConfigWithIncorrectFontColorName_ShouldThrowArgumentException(string font)
        {
            config.FontColor = font;

            var act = () => MyConfigValidator.ValidateConfig(config);

            act.Should().Throw<ArgumentException>().WithMessage("Invalid font color");
        }

        [TestCase("")]
        [TestCase("NonExistingColor")]
        public void ValidateConfig_AddConfigWithIncorrectBackgroundColorName_ShouldThrowArgumentException(string font)
        {
            config.BackgroundColor = font;

            var act = () => MyConfigValidator.ValidateConfig(config);

            act.Should().Throw<ArgumentException>().WithMessage("Invalid backgroud color");
        }
    }
}