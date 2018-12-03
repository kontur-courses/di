using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud
{
    [TestFixture]
    public class TagCloudStatsGenerator_Should
    {
        private TagCloudStatsGenerator _tagCloudStatsGenerator;
        private string[] _words;

        [SetUp]
        public void SetUp()
        {
            _tagCloudStatsGenerator = new TagCloudStatsGenerator();
            _words = "Lorem ipsum dolor sit amet.".Split(' ');
        }

        [Test]
        public void ReturnEachWord_WhenEveryWordIsDoubled()
        {
            var repeatedWords = _words.Concat(_words);
            _tagCloudStatsGenerator.GenerateStats(repeatedWords)
                                   .Should()
                                   .OnlyContain(w => w.Count == 2);

        }

        
    }
}