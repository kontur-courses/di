using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsColoringAlgorithms;

namespace TagCloudContainerTests
{
    public class DefaultWordsStainerPainter
    {
        private DefaultWordsPainter painter;

        [SetUp]
        public void SetUp()
        {
            painter = new DefaultWordsPainter();
        }

        [Test]
        public void DefaultWordStainer_ShouldReturnSequenceWithOneColor()
        {
            var result =
                painter.GetWordColorDictionary(new Dictionary<string, int> {{"asdx", 2}, {"ssds", 1}, {"ssdsas", 1}},
                    Color.Black);
            result.Should().BeEquivalentTo(new Dictionary<string, Color>{{"asdx", Color.Black}, {"ssds", Color.Black}, {"ssdsas", Color.Black}});
        }
    }
}