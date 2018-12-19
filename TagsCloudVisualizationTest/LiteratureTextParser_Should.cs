using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.WordsFileReading;

namespace TagsCloudVisualizationTest
{
    [TestFixture]
    class LiteratureTextParser_Should
    {
        private readonly LiteratureTextParser parser = new LiteratureTextParser();

        [Test]
        public void ReturnSingleWords_WhenOnlyOneWord()
        {
            parser.ParseText("word")
                .Should().BeEquivalentTo("word");
        }

        [Test]
        public void ReturnNoWords_WhenOnlySeparators()
        {
            parser.ParseText("., ")
                .Should().BeEmpty();
        }

        [Test]
        public void ReturnAllWords_WhenSeparatedBySingleChar()
        {
            parser.ParseText("w w w")
                .Should().BeEquivalentTo("w", "w", "w");
        }

        [Test]
        public void ReturnAllWords_WhenSeparatedBySequenceOfChars()
        {
            parser.ParseText("w , w \n. w")
                .Should().BeEquivalentTo("w", "w", "w");
        }
    }
}
