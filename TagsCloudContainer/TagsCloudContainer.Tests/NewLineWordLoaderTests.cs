using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsLoading;

namespace TagsCloudContainer.Tests
{
    public class NewLineWordLoaderTests
    {
        private const string testFile = "test.txt";
        private Lazy<NewLineWordLoader> loader;

        [SetUp]
        public void SetUp()
        {
            loader = new Lazy<NewLineWordLoader>(() => new NewLineWordLoader(testFile));
        }

        [Test]
        public void Load_WithExistingFile_LoadWords()
        {
            var words = new List<string> {"привет", "мир", "как", "ты", "поживаешь"};
            File.WriteAllLines(testFile, words);
            loader.Value.GetWords()
                .Should().BeEquivalentTo(words);
        }

        [Test]
        public void Constructor_WithNotExistingFile_ThrowsApplicationException() =>
            Assert.Throws<ApplicationException>(() => new NewLineWordLoader("fefsf"));

        [Test]
        public void Constructor_WithNull_ThrowsArgumentException() =>
            Assert.Throws<ArgumentNullException>(() => new NewLineWordLoader(null));

        [TestCaseSource(nameof(GetWordsFromEncodingTestCases))]
        public void GetWords_FromEncoding(Encoding encoding)
        {
            var words = new List<string> {"привет", "мир", "как", "ты", "поживаешь"};
            File.WriteAllLines(testFile, words, encoding);
            loader.Value.GetWords()
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