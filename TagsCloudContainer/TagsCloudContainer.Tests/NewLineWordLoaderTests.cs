using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordLoading;

namespace TagsCloudContainer.Tests
{
    public class NewLineWordLoaderTests
    {
        private const string testFile = "test.txt";
        private NewLineWordLoader loader;

        [SetUp]
        public void SetUp()
        {
            loader = new NewLineWordLoader();
        }

        [Test]
        public void Load_WithExistingFile_LoadWords()
        {
            var words = new List<string> {"привет", "мир", "как", "ты", "поживаешь"};
            File.WriteAllLines(testFile, words);
            loader.Load(testFile)
                .Should().BeEquivalentTo(words);
        }

        [Test]
        public void Load_WithNotExistingFile_ThrowsApplicationException() =>
            Assert.That(() => loader.Load("sef"), Throws.InstanceOf<ApplicationException>());

        [Test]
        public void Load_WithNull_ThrowsArgumentException() =>
            Assert.That(() => loader.Load(null), Throws.InstanceOf<ArgumentException>());

        [TestCaseSource(nameof(LoadFromEncodingTestCases))]
        public void Load_FromEncoding(Encoding encoding)
        {
            var words = new List<string> {"привет", "мир", "как", "ты", "поживаешь"};
            File.WriteAllLines(testFile, words, encoding);
            loader.Load(testFile)
                .Should().BeEquivalentTo(words);
        }

        private static IEnumerable<TestCaseData> LoadFromEncodingTestCases()
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