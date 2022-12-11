using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    public class CustomOptionsValidatorTests
    {
        private CustomOptions options;

        [SetUp]
        public void Setup()
        {
            options = new CustomOptions
            {
                WorkingDir = "c:\\Windows\\System32",
                WordsFileName = "cmd.exe",
                BoringWordsName = "cmd.exe",
                Font = "Arial",
                PictureSize = 600,
                MinTagSize = 15,
                BackgroundColor = "White",
                FontColor = "Blue"
            };
        }

        [Test]
        public void ValidateConfig_AddPreSetConfig_ShouldNotThrowAnyExceptions()
        {
            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().NotThrow();
        }

        [Test]
        public void ValidateConfig_AddPreSetConfigWithLowerCaseColor_ShouldNotThrowAnyExceptions()
        {
            options.FontColor = "white";

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().NotThrow();
        }

        [Test]
        public void ValidateConfig_AddConfigWithEmptyTextsPath_ShouldThrowArgumentException()
        {
            options.WorkingDir = "";

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Texts directory does not exist");
        }

        [Test]
        public void ValidateConfig_AddConfigWithEmptyWordsFileName_ShouldThrowArgumentException()
        {
            options.WordsFileName = "";

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Tag file does not exist");
        }

        [Test]
        public void ValidateConfig_AddConfigWithEmptyBoringWordsFileName_ShouldThrowArgumentException()
        {
            options.BoringWordsName = "";

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Exclude words file does not exist");
        }

        [TestCase("")]
        [TestCase("NonExistingFont")]
        public void ValidateConfig_AddConfigWithIncorectFontName_ShouldThrowArgumentException(string font)
        {
            options.Font = font;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage($"Font \"{font}\" can't be found");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateConfig_AddConfigWithPictureSizeLessThanOne_ShouldThrowArgumentException(int size)
        {
            options.PictureSize = size;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Picture size should be above 0");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateConfig_AddConfigWithFontSizeLessThanOne_ShouldThrowArgumentException(int size)
        {
            options.MinTagSize = size;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Font size should be above 0");
        }

        [TestCase("")]
        [TestCase("NonExistingColor")]
        public void ValidateConfig_AddConfigWithIncorrectFontColorName_ShouldThrowArgumentException(string font)
        {
            options.FontColor = font;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Invalid font color");
        }

        [TestCase("")]
        [TestCase("NonExistingColor")]
        public void ValidateConfig_AddConfigWithIncorrectBackgroundColorName_ShouldThrowArgumentException(string font)
        {
            options.BackgroundColor = font;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Invalid backgroud color");
        }

        [TestCase(600)]
        [TestCase(601)]
        public void ValidateConfig_AddConfigWithFontSizeMoreOrEqualThanPictureSize_ShouldThrowArgumentException(
            int size)
        {
            options.MaxTagSize = size;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Font size should be less than picture size");
        }
    }
}