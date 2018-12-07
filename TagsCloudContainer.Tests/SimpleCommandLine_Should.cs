using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class SimpleCommandLine_Should
    {
        private SimpleCommandLineParser parser = new SimpleCommandLineParser();
        private readonly string pathToWordsFile = "words.txt";
        private readonly string directoryToSave = @"C:\Users\User\Pictures";
        private readonly string fileName = "cvTRp";

        [Test]
        public void Parse_RequiredArguments_Successfully()
        {
            var args = new[] {pathToWordsFile, directoryToSave, fileName};

            var parsed = parser.Parse(args);

            parsed.PathToWordsFile
                .Should()
                .BeEquivalentTo("words.txt");
            parsed.DirectoryToSave
                .Should()
                .BeEquivalentTo(@"C:\Users\User\Pictures");
            parsed.OutFileName
                .Should()
                .BeEquivalentTo("cvTRp");
        }

        [Test]
        public void Parse_OptionalArguments_Successfully()
        {
            var args = new[]
            {
                pathToWordsFile,
                directoryToSave,
                fileName,
                "-f", "Times New Roman",
                "-c", "Red",
                "-s", "28",
                "-w", "4096",
                "-h", "2048"
            };
            var expected = new SimpleConfiguration()
            {
                PathToWordsFile = "words.txt",
                DirectoryToSave = @"C:\Users\User\Pictures",
                OutFileName = "cvTRp",
                Color = "Red",
                FontFamily = "Times New Roman",
                FontSize = 28,
                ImageWidth = 4096,
                ImageHeight = 2048
            };

            parser.Parse(args).Should()
                .BeEquivalentTo(expected);
        }

        [Test]
        public void Parse_OptionalArgumentsNotSpecified_DefaultValues()
        {
            var args = new[] {pathToWordsFile, directoryToSave, fileName};
            var expected = new SimpleConfiguration()
            {
                PathToWordsFile = "words.txt",
                DirectoryToSave = @"C:\Users\User\Pictures",
                OutFileName = "cvTRp",
                Color = "Black",
                FontFamily = "Arial",
                FontSize = 32,
                ImageHeight = 1024,
                ImageWidth = 2048
            };

            parser.Parse(args).Should()
                .BeEquivalentTo(expected);
        }
    }
}