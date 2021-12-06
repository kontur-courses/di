using System;
using System.Globalization;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsProvider;

namespace TagsCloudVisualization.Tests.WordsProvider
{
    [TestFixture]
    public class WordsFromFileProviderTests
    {
        private static readonly string FileName = Path.Combine(Directory.GetCurrentDirectory(), "Temp.txt");
        private WordsFromTxtFileProvider _provider;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            File.Create(FileName).Close();
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            File.Delete(FileName);
        }

        [SetUp]
        public void Setup()
        {
            _provider = new WordsFromTxtFileProvider(FileName);
        }

        [TearDown]
        public void TearDown()
        {
            File.WriteAllText(FileName, string.Empty);
        }

        [Test]
        public void GetWords_ShouldReturnEmptyCollection_WhenNoWords()
        {
            File.WriteAllText(FileName, string.Empty);

            var words = _provider.GetWords();

            words.Should().BeEmpty();
        }

        [TestCase(new object[]
        {
            "first"
        })]
        [TestCase(new object[]
        {
            "first",
            "second",
            "fifth"
        })]
        public void GetWords_ShouldReturnWords_WhenFileHasWords(params string[] expectedWords)
        {
            File.WriteAllLines(FileName, expectedWords);

            var words = _provider.GetWords();

            words.Should().ContainInOrder(expectedWords);
        }

        [Test]
        public void GetWords_ShouldThrowException_WhenFileNotExists()
        {
            var provider = new WordsFromTxtFileProvider(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            Assert.Throws<Exception>(() => provider.GetWords());
        }
    }
}