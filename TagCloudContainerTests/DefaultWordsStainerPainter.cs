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
        public void DefaultWordStainer_ShouldThrowException_OnUnknownBrushColor()
        {
            var unknownColor = "Timeless";
            Action act = () => painter.GetColorsSequence(new Dictionary<string, int>{{"asdx", 2}, {"ssds", 1}}, unknownColor);
            act.Should().Throw<ArgumentException>().WithMessage("Unknown brush color");
        }

        [Test]
        public void DefaultWordStainer_ShouldReturnSequenceWithOneColor()
        {
            var color = "Black";
            var result = painter.GetColorsSequence(new Dictionary<string, int>{{"asdx", 2}, {"ssds", 1}, {"ssdsas", 1}}, color);
            result.Should().BeEquivalentTo(new Color[] {Color.Black, Color.Black, Color.Black});
        }
    }
}