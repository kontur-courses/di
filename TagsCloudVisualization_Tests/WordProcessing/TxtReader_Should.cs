using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization_Tests.WordProcessing
{
    [TestFixture]
    public class TxtReader_Should
    {
        private string filename;
        private TxtReader reader;

        [SetUp]
        public void SetUp()
        {
            filename = Path.Combine(TestContext.CurrentContext.TestDirectory, @"WordProcessing\testFile.txt");
            reader = new TxtReader(filename);
        }

        [Test]
        public void ProvideWords_ReturnWordsCorrectly()
        {
            reader.Provide().Should().BeEquivalentTo("fff", "dddd");
        }
    }
}
