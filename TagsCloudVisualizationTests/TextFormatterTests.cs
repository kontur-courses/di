using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TextFormatters;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class TextFormatterTests
    {
        private TextFormatter textFormatter;

        [SetUp]
        public void Setup()
        {
            textFormatter = new TextFormatter(new SmallWordFilter());
        }

        [Test]
        public void WordFormatter_ShouldSkipShortWords_WhenSmallWordFilter()
        {
            string input = "do\neiusmod\ntempor\nincididunt\nut";
            
            var expectedWords = new List<string>() { "eiusmod", "tempor", "incididunt" };

            textFormatter.Format(input).Select(t => t.Value).ToList().Should().BeEquivalentTo(expectedWords);
        }

        [Test]
        public void WordFormatter_ShouldCountWordFrequency_WhenSmallWordFilter()
        {
            string input = "do\neiusmod\ntempor\nincididunt\nut\ndo\neiusmod\ntempor\ntempor";

            var expected = new List<float>() { 3f/6, 2f/6, 1f/6 };
            var result = textFormatter.Format(input).Select(t => t.Frequency);
            result.Should().BeEquivalentTo(expected);
        }
    }
}
