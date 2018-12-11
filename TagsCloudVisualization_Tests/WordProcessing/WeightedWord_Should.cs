using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization_Tests.WordProcessing
{
    [TestFixture]
    public class WeightedWord_Should
    {
        private WeightedWord word;
        [SetUp]
        public void SetUp()
        {
            word = new WeightedWord("aaa", 22);
        }

        [Test]
        public void CompareTo_EqualWord_Return_0()
        {
            word.CompareTo(new WeightedWord("ccc", 22)).Should().Be(0);
        }

        [Test]
        public void CompareTo_LessWord_Return_1()
        {
            word.CompareTo(new WeightedWord("ccc", 20)).Should().Be(1);
        }

        [Test]
        public void CompareTo_LessWord_Return_Minus1()
        {
            word.CompareTo(new WeightedWord("ccc", 25)).Should().Be(-1);
        }
    }
}
