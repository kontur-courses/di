using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud
{
    [TestFixture]
    public class TagCloudStatsGeneratorShould
    {
        private TagCloudStatsGenerator tagCloudStatsGenerator;
        private string[] words;

        [SetUp]
        public void SetUp()
        {
            this.tagCloudStatsGenerator = new TagCloudStatsGenerator();
            this.words = "Lorem ipsum dolor sit amet.".Split(' ');
        }

        [Test]
        public void ReturnEachWord_WhenEveryWordIsDoubled()
        {
            var repeatedWords = this.words.Concat(this.words);
            this.tagCloudStatsGenerator.GenerateStats(repeatedWords)
                                   .Should()
                                   .OnlyContain(w => w.Count == 2);

        }

        
    }
}