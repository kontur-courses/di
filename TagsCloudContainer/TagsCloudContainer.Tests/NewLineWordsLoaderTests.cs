using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsLoading;

namespace TagsCloudContainer.Tests
{
    public class NewLineWordsLoaderTests
    {
        private const string testFile = "test.txt";
        private NewLineWordsLoader loader;

        [SetUp]
        public void SetUp()
        {
            loader = new NewLineWordsLoader();
        }

        [Test]
        public void LoadWords_WithExistingFile_LoadWords()
        {
            var words = new List<string> {"привет", "мир", "как", "ты", "поживаешь"};
            File.WriteAllLines(testFile, words);
            loader.LoadWords(testFile)
                .Should().BeEquivalentTo(words);
        }

        [Test]
        public void LoadWords_WithNotExistingFile_ThrowsApplicationException() =>
            Assert.Throws<ApplicationException>(() => loader.LoadWords("123"));

        [Test]
        public void LoadWords_WithNull_ThrowsArgumentException() =>
            Assert.Throws<ArgumentNullException>(() => loader.LoadWords(null));

        [TestCaseSource(nameof(GetWordsFromEncodingTestCases))]
        public void LoadWords_FromEncoding(Encoding encoding)
        {
            var words = new List<string> {"привет", "мир", "как", "ты", "поживаешь"};
            File.WriteAllLines(testFile, words, encoding);
            loader.LoadWords(testFile)
                .Should().BeEquivalentTo(words);
        }

        private static IEnumerable<TestCaseData> GetWordsFromEncodingTestCases()
        {
            yield return new TestCaseData(Encoding.Unicode) {TestName = "Unicode"};
            yield return new TestCaseData(Encoding.BigEndianUnicode) {TestName = "BigEndianUnicode"};
            yield return new TestCaseData(Encoding.UTF8) {TestName = "UTF8"};
            yield return new TestCaseData(Encoding.UTF32) {TestName = "UTF32"};
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(testFile))
                File.Delete(testFile);
        }
    }
}