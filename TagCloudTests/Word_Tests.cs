using FluentAssertions;
using NUnit.Framework;
using WordCloud.TextAnalyze.Words;

namespace TagCloudTests
{
    [TestFixture]
    public class Word_Tests
    {
        [Test]
        public void Ctor_WordContainsGivenText()
        {
            var word = new Word("text", 1);
            var expected = "text";
            word.Text.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Ctor_WordContainsGivenEntries()
        {
            var word = new Word("text", 1);
            var expected = 1;
            word.Entries.Should().BeOneOf(expected);
        }
    }
}
