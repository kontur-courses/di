using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudApp.WordsLoading;

namespace TagsCloud.Tests
{
    public class TxtFileTextLoaderTests
    {
        private const string testFile = "test.txt";
        private TxtFileTextLoader loader;

        [SetUp]
        public void SetUp()
        {
            loader = new TxtFileTextLoader();
        }

        [TestCaseSource(nameof(GetWordsFromEncodingTestCases))]
        public void LoadText_FromFileWithEncoding_LoadText(Encoding encoding)
        {
            var text = "Hello world!";
            File.WriteAllText(testFile, text, encoding);
            loader.LoadText(testFile)
                .Should().Be(text);
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