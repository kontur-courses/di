using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordProcessor;

namespace TagsCloudContainer.NewTests
{
    class BasicWordProcessorTests
    {
        private readonly BasicWordProcessor wordProcessor = new BasicWordProcessor();

        [Test]
        public void ProcessWords_ShouldRemoveDuplicateWords()
        {
            var words = new List<string> { "Two", "One", "Two" };

            var processedWords = wordProcessor.ProcessWords(words).ToList();

            var expected = new List<WordWithCount>
            {
                new WordWithCount("Two", 2),
                new WordWithCount("One", 1)
            };

            processedWords.Should().BeEquivalentTo(expected);
        }
    }
}
