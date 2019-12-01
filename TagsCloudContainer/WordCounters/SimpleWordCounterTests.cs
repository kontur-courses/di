using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordCounters
{
    [TestFixture]
    class SimpleWordCounterTests
    {
        [Test]
        public void CountWords()
        {
            var counter = new SimpleWordCounter();
            var words = new List<string> { "a", "d", "d", "j", "a", "h", "a" };
            var expect = new List<WordToken>
            {
                new WordToken("a", 3),
                new WordToken("d", 2),
                new WordToken("j", 1),
                new WordToken("h", 1),
            };

            var result = counter.CountWords(words);

            result.Should().BeEquivalentTo(expect);
        }
    }
}
