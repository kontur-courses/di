using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using TagCloud.TextReaders;

namespace Tests.TextReaders
{
    [TestFixture]
    public class TxtTextReader_Should
    {
        private string baseDir;
        private TxtWordsReader txtWordsReader;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            baseDir = TestContext.CurrentContext.TestDirectory + @"\..\..\Resources\";
            txtWordsReader = new TxtWordsReader();
        }

        [Test]
        public void ReadOneWord_WhenFileHasOnlyOneWord()
        {
            var expectedRes = new List<string> {"oneword"};
            var res = txtWordsReader.ReadFrom(baseDir + "oneword.txt");
            res.Should().BeEquivalentTo(expectedRes);
        }

        [Test]
        public void ReadFewWords_WhenFileHasFewWords()
        {
            var expectedRes = new List<string> { "Hello", "world", "Heh" };
            var res = txtWordsReader.ReadFrom(baseDir + "fewwords.txt");
            res.Should().BeEquivalentTo(expectedRes);
        }
    }
}