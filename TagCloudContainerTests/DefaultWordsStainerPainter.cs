using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsColoringAlgorithms;

namespace TagCloudContainerTests
{
    public class DefaultWordsStainerPainter
    {
        private DefaultWordPainter painter;

        [SetUp]
        public void SetUp()
        {
            painter = new DefaultWordPainter();
        }

        [Test]
        public void DefaultWordStainer_ShouldThrowException_OnUnknownBrushColor()
        {
            var unknownColor = "Timeless";
            Action act = () => painter.GetColorsSequence(10, unknownColor);
            act.Should().Throw<ArgumentException>().WithMessage("Unknown brush color");
        }

        [Test]
        public void DefaultWordStainer_ShouldReturnSequenceWithOneColor()
        {
            var color = "Black";
            var result = painter.GetColorsSequence(3, color);
            result.Should().BeEquivalentTo(new Color[] {Color.Black, Color.Black, Color.Black});
        }
    }
}