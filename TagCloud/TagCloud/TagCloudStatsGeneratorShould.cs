using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud
{
    [TestFixture]
    public class TagCloudStatsGeneratorShould
    {
        [SetUp]
        public void SetUp()
        {
            tagCloudStatsGenerator = new TagCloudStatsGenerator();
            words = "Lorem ipsum dolor sit amet.".Split(' ');
        }

        private TagCloudStatsGenerator tagCloudStatsGenerator;
        private string[] words;

        [Test]
        public void ReturnEachWord_WhenEveryWordIsDoubled()
        {
            var repeatedWords = words.Concat(words);
            tagCloudStatsGenerator.GenerateStats(repeatedWords)
                .Should()
                .OnlyContain(w => w.Count == 2);
        }
    }
}
