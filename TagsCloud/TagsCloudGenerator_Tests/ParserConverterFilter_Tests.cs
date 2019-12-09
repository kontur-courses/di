using System.IO;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudGenerator.WordsParsers;
using TagsCloudGenerator.WordsConverters;
using TagsCloudGenerator.WordsFilters;
using TagsCloudGeneratorExtensions;

namespace TagsCloudGenerator_Tests
{
    internal class ParserConverterFilter_Tests
    {
        private readonly string workingDirectory =
            string.Join(
                Path.DirectorySeparatorChar.ToString(),
                "TagsCloudGenerator_Tests", "TestData") + Path.DirectorySeparatorChar;

        private SingletonScopeInstancesContainer container;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            container = new SingletonScopeInstancesContainer();
            Metadata.PathToMyStem = "mystem.exe";
        }

        [TearDown]
        public void TearDown() => container.Get<Settings>().Reset();

        [TestCase(
            "TestData.txt",
            "ноутбук", "конверт", "хлеб", "тарелка", "тарелки", "клавиатура", "пробел")]
        public void UTF8LinesParserToLowerConverterBoringWordsFilter_Test(
            string pathToRead,
            params string[] expected)
        {
            var linesParser = container.Get<UTF8LinesParser>();
            var toLowerConverter = container.Get<WordsToLowerConverter>();
            var boringWordsFilter = container.Get<BoringWordsFilter>();

            var words = linesParser.ParseFromFile(workingDirectory + pathToRead);
            var wordsToLower = toLowerConverter.Execute(words);
            var actual = boringWordsFilter.Execute(wordsToLower);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(
            "TestData.docx",
            "ноутбук", "конверт", "хлеб", "тарелка", "тарелка", "клавиатура", "пробел")]
        public void DocxLinesParserToLowerAndInitialWordFormConvertersBoringWordsFilter_Test(
            string pathToRead,
            params string[] expected)
        {
            var docxLinesParser = container.Get<DocxLinesParser>();
            var toLowerConverter = container.Get<WordsToLowerConverter>();
            var initialWordFormConverter = container.Get<InitialWordsFormConverter>();
            var boringWordsFilter = container.Get<BoringWordsFilter>();

            var words = docxLinesParser.ParseFromFile(workingDirectory + pathToRead);
            var toLower = toLowerConverter.Execute(words);
            var toInitialWordForm = initialWordFormConverter.Execute(toLower);
            var actual = boringWordsFilter.Execute(toInitialWordForm);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(
            "TestDataWithVerbs.txt",
            "делать", "бежать", "указать")]
        public void UTF8LinesParserToLowerConverterBoringWordsAndTakenVerbFilter(
            string pathToRead,
            params string[] expected)
        {
            var linesParser = container.Get<UTF8LinesParser>();
            var toLowerConverter = container.Get<WordsToLowerConverter>();
            var boringWordsFilter = container.Get<BoringWordsFilter>();
            var takenVerbsFilter = container.Get<TakenPartsOfSpeechFilter>();
            container.Get<Settings>().TakenPartsOfSpeech = new[] { "v" };

            var words = linesParser.ParseFromFile(workingDirectory + pathToRead);
            var toLower = toLowerConverter.Execute(words);
            var noBoringWords = boringWordsFilter.Execute(toLower);
            var actual = takenVerbsFilter.Execute(noBoringWords);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}