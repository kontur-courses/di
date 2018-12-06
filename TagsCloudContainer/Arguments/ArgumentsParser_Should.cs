using FluentAssertions;
using NUnit.Framework;
using System.Drawing;

namespace TagsCloudContainer.Arguments
{
    [TestFixture]
    public class ArgumentsParser_Should
    {
        [Test]
        public void Constructor_CorrectInputAndOutput()
        {
            var args = new string[] { "-i", "./input/input.txt", "-o", "./output/o.png" };
            var argumentsParser = new ArgumentsParser(args);

            argumentsParser.InputPath.Should().Be("./input/input.txt");
            argumentsParser.OutputPath.Should().Be("./output/o.png");
        }

        [Test]
        public void Constructor_OnlyInput()
        {
            var args = new string[] { "-i", "./input/input.txt" };
            var argumentsParser = new ArgumentsParser(args);

            argumentsParser.InputPath.Should().Be(null);
        }

        [Test]
        public void Constructor_OnlyOutput()
        {
            var args = new string[] { "-o", "./output/o.png" };
            var argumentsParser = new ArgumentsParser(args);

            argumentsParser.OutputPath.Should().Be(null);
        }

        [Test]
        public void Constructor_OnlyColor()
        {
            var args = new string[] { "-c", "red" };
            var argumentsParser = new ArgumentsParser(args);

            argumentsParser.Brush.Should().Be(null);
        }

        [Test]
        public void Constructor_Correct()
        {
            var args = new string[] { "-i", "./input/input.txt", "-o", "./output/o.png", "-c", "red",
                "--words-to-exclude", "./input/words to exclude.txt", "-f", "Arial" };

            var argumentsParser = new ArgumentsParser(args);

            argumentsParser.InputPath.Should().Be("./input/input.txt");
            argumentsParser.OutputPath.Should().Be("./output/o.png");
            argumentsParser.Brush.Should().Be(Brushes.Red);
            argumentsParser.WordsToExcludePath.Should().Be("./input/words to exclude.txt");
            argumentsParser.FontName.Should().Be("Arial");
        }
    }

}
