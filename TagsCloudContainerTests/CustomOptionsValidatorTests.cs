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
                MaxTagSize = 30,
                BackgroundColor = "White",
                FontColor = "Blue",
                ImageFormat = "png"
            };
        }

        [Test]
        public void ValidateConfig_AddPreSetOptions_ShouldNotThrowAnyExceptions()
        {
            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().NotThrow();
        }

        [Test]
        public void ValidateConfig_AddLowerCaseColor_ShouldNotThrowAnyExceptions()
        {
            options.FontColor = "white";

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().NotThrow();
        }

        [Test]
        public void ValidateConfig_AddEmptyTextsPath_ShouldThrowArgumentException()
        {
            options.WorkingDir = "";

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Texts directory does not exist");
        }

        [Test]
        public void ValidateConfig_AddEmptyWordsFileName_ShouldThrowArgumentException()
        {
            options.WordsFileName = "";

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Tag file does not exist");
        }

        [Test]
        public void ValidateConfig_AddEmptyBoringWordsFileName_ShouldThrowArgumentException()
        {
            options.BoringWordsName = "";

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Exclude words file does not exist");
        }

        [TestCase("")]
        [TestCase("NonExistingFont")]
        public void ValidateConfig_AddIncorectFontName_ShouldThrowArgumentException(string font)
        {
            options.Font = font;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage($"Font \"{font}\" can't be found");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateConfig_AddPictureSizeLessThanOne_ShouldThrowArgumentException(int size)
        {
            options.PictureSize = size;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Picture size should be above 0");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateConfig_AddMaxFontMoreThamPictureSize_ShouldThrowArgumentException(int size)
        {
            options.MinTagSize = size;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Font size should be above 0");
        }

        [TestCase("")]
        [TestCase("NonExistingColor")]
        public void ValidateConfig_AddIncorrectFontColorName_ShouldThrowArgumentException(string font)
        {
            options.FontColor = font;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Invalid font color");
        }

        [TestCase("")]
        [TestCase("NonExistingColor")]
        public void ValidateConfig_AddIncorrectBackgroundColorName_ShouldThrowArgumentException(string font)
        {
            options.BackgroundColor = font;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Invalid backgroud color");
        }

        [TestCase(600)]
        [TestCase(601)]
        public void ValidateConfig_AddFontSizeMoreOrEqualThanPictureSize_ShouldThrowArgumentException(
            int size)
        {
            options.MaxTagSize = size;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Font size should be less than picture size");
        }

        [TestCase("png")]
        [TestCase("PNG")]
        public void ValidateConfig_AddSupportedFormat_ShouldNotThrowExceptions(string format)
        {
            options.ImageFormat = format;

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().NotThrow();
        }

        [Test]
        public void ValidateConfig_AddUsupportedFormat_ShouldNotThrowExceptions()
        {
            options.ImageFormat = "ping";

            var act = () => CustomOptionsValidator.ValidateOptions(options);

            act.Should().Throw<ArgumentException>().WithMessage("Unsupported image format");
        }
    }
}