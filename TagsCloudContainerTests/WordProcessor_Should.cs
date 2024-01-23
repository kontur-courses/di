using FluentAssertions;
using NUnit.Framework;
using StopWord;
using TagsCloudContainer.Algorithm;

namespace TagsCloudContainerTests
{
    public class WordProcessor_Should
    {
        private WordProcessor wordProcessor;

        [SetUp]
        public void SetUp()
        {
            wordProcessor = new WordProcessor(new FileParser());
        }

        [Test]
        public void GetInterestingWords_WhenFileWithBoringWordsIsEmpty()
        {
            var sourceFilePath = GetProjectDirectory() + @"\src\sourceData.txt";
            var boringFilePath = GetProjectDirectory() + @"\src\emptyData.txt";
            var stopWords = StopWords.GetStopWords("ru");

            var interestingWords = wordProcessor.GetInterestingWords(sourceFilePath, boringFilePath);

            interestingWords.Should().NotContain(stopWords);
        }

        [Test]
        public void GetInterestingWords_WhenFileWithBoringWordsIsNotEmpty()
        {
            var sourceFilePath = GetProjectDirectory() + @"\src\sourceData.txt";
            var boringFilePath = GetProjectDirectory() + @"\src\boringData.txt";
            var stopWords = StopWords.GetStopWords("ru");

            var interestingWords = wordProcessor.GetInterestingWords(sourceFilePath, boringFilePath);
            var boringWords = new FileParser().ReadWordsInFile(boringFilePath);

            interestingWords.Should().NotContain(stopWords);
            interestingWords.Should().NotContain(boringWords);
        }

        [Test]
        public void CorrectlyCalculatesFrequencyWords()
        {
            var expectedDictonary = new Dictionary<string, int>
            {
                { "js", 3 },
                { "python", 4},
                { "c#", 5 },
                { "c++", 2 },
                { "rust", 2 },
                { "go", 4 },
                { "1c", 1 }
            };
            var sourceFilePath = GetProjectDirectory() + @"\src\sourceData.txt";
            var boringFilePath = GetProjectDirectory() + @"\src\emptyData.txt";

            var wordsFrequency = wordProcessor.CalculateFrequencyInterestingWords(sourceFilePath, boringFilePath);

            wordsFrequency.Should().Contain(expectedDictonary);
        }

        [Test]
        public void GiveFrequencyWordsDescendingOrder()
        {
            var expectedDictonary = new Dictionary<string, int>
            {
                { "js", 3 },
                { "python", 4},
                { "c#", 5 },
                { "c++", 2 },
                { "rust", 2 },
                { "go", 4 },
                { "1c", 1 }
            };
            var sourceFilePath = GetProjectDirectory() + @"\src\sourceData.txt";
            var boringFilePath = GetProjectDirectory() + @"\src\emptyData.txt";

            var wordsFrequency = wordProcessor.CalculateFrequencyInterestingWords(sourceFilePath, boringFilePath);

            wordsFrequency.Should().BeInDescendingOrder(word => word.Value);
        }

        [Test]
        public void ReturnEmptyDictionary_WhenSourceWordFileIsEmpty()
        {
            var sourceFilePath = GetProjectDirectory() + @"\src\emptyData.txt";
            var boringFilePath = GetProjectDirectory() + @"\src\emptyData.txt";

            var wordsFrequency = wordProcessor.CalculateFrequencyInterestingWords(sourceFilePath, boringFilePath);

            wordsFrequency.Should().BeEmpty();
        }

        public string GetProjectDirectory()
        {
            var binDirectory = AppContext.BaseDirectory;
            var projectDirectory = Directory.GetParent(binDirectory).Parent.Parent.Parent.FullName;

            return projectDirectory;
        }
    }
}
