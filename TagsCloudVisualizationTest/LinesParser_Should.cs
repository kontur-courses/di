using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsFileReading;

namespace TagsCloudVisualizationTest
{
    [TestFixture]
    class LinesParser_Should
    {
        private readonly LinesParser parser = new LinesParser();

        [Test]
        public void ReturnSingleElement_WhenOnlyOneLine()
        {
            parser.ParseText(new StringReader("text"))
                .Should().BeEquivalentTo("text");
        }

        [Test]
        public void TrimLine()
        {
            parser.ParseText(new StringReader("  text  "))
                .Should().BeEquivalentTo("text");
        }

        [Test]
        public void ReturnAllLines_WhenSeveralLines()
        {
            parser.ParseText(new StringReader(
                    "line1" + Environment.NewLine + "line2"))
                .Should().BeEquivalentTo("line1", "line2");
        }

        [Test]
        public void SkipEmptyLines()
        {
            parser.ParseText(new StringReader(
                    "line1" + Environment.NewLine + "" + Environment.NewLine + "line3"))
                .Should().BeEquivalentTo("line1", "line3");
        }
    }
}
