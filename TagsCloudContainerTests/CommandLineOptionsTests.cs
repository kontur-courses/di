using CommandLine;
using NUnit.Framework;
using TagsCloudContainer.Utility;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class CommandLineOptionsTests
    {
        [Test]
        public void ParsingWithValidArguments_ShouldSucceed()
        {
            var args = new[] { "-f", "Georgia", "-w", "1500", "-t", "src/text.txt" };

            var parseResult = Parser.Default.ParseArguments<CommandLineOptions>(args);

            Assert.IsTrue(parseResult.Tag == ParserResultType.Parsed);
            Assert.IsInstanceOf<CommandLineOptions>(parseResult.Value);

            var options = parseResult.Value;

            Assert.That(options.FontName, Is.EqualTo("Georgia"));
            Assert.That(options.ImageWidth, Is.EqualTo(1500));
            Assert.That(options.TextFilePath, Is.EqualTo("src/text.txt"));         
        }

        [Test]
        public void ParsingWithoutArguments_ShouldUseDefaultValues()
        {
            var args = Array.Empty<string>();

            var parseResult = Parser.Default.ParseArguments<CommandLineOptions>(args);

            Assert.IsTrue(parseResult.Tag == ParserResultType.Parsed);
            Assert.IsInstanceOf<CommandLineOptions>(parseResult.Value);

            var options = parseResult.Value;


            Assert.That(options.FontName, Is.EqualTo("Verdana"));
            Assert.That(options.ImageWidth, Is.EqualTo(1600));
            Assert.That(options.TextFilePath, Is.EqualTo("src/text.txt"));
            Assert.That(options.FontColor, Is.EqualTo("Green"));
        }
    }
}
