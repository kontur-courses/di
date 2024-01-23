using FluentAssertions;
using TagsCloudPainter.FileReader;
using TagsCloudPainter.Parser;
using TagsCloudPainter.Settings;

namespace TagsCloudPainterTests
{
    [TestFixture]
    public class BoringTextParserTests
    {
        private TextSettings textSettings;
        private BoringTextParser boringTextParser;
        private TextFileReader textFileReader;

        [SetUp]
        public void Setup()
        {
            textFileReader = new TextFileReader();            
            var boringText = textFileReader
                .ReadFile(@$"{Environment.CurrentDirectory}..\..\..\..\TextFiles\boringWords.txt");
            textSettings = new TextSettings() { BoringText = boringText};
            boringTextParser = new BoringTextParser(textSettings);
        }

        [Test]
        public void ParseText_ShouldReturnWordsListWithoutBoringWords()
        {
            var boringWords = boringTextParser
                .GetBoringWords(textFileReader.ReadFile(@$"{Environment.CurrentDirectory}..\..\..\..\TextFiles\boringWords.txt"))
                .ToHashSet();
            var parsedText = boringTextParser
                .ParseText(textFileReader.ReadFile(@$"{Environment.CurrentDirectory}..\..\..\..\TextFiles\testFile.txt"));
            var isBoringWordsInParsedText = parsedText.Where(boringWords.Contains).Any();
            isBoringWordsInParsedText.Should().BeFalse();
        }

        [Test]
        public void ParseText_ShouldReturnNotEmptyWordsList_WhenPassedNotEmptyText()
        {
            var parsedText = boringTextParser
                .ParseText(textFileReader.ReadFile(@$"{Environment.CurrentDirectory}..\..\..\..\TextFiles\testFile.txt"));
            parsedText.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void ParseText_ShouldReturnWordsInLowerCase()
        {
            var parsedText = boringTextParser
                .ParseText(textFileReader.ReadFile(@$"{Environment.CurrentDirectory}..\..\..\..\TextFiles\testFile.txt"));
            var isAnyWordNotLowered = parsedText.Where(word => word.ToLower() != word).Any();
            isAnyWordNotLowered.Should().BeFalse();
        }

        [Test]
        public void ParseText_ShouldReturnWordsListWithTheSameAmountAsInText()
        {
            var parsedText = boringTextParser
                .ParseText(textFileReader.ReadFile(@$"{Environment.CurrentDirectory}..\..\..\..\TextFiles\testFile.txt"));
            parsedText.Count.Should().Be(20);
        }
    }
}
