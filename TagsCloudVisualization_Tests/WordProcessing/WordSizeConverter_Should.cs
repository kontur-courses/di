using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization_Tests.WordProcessing
{

    public class WordSizeConverter_Should
    {
        private FontSettings fontSettings;
        private WordSizeConverter converter;

        [SetUp]
        public void SetUp()
        {
            fontSettings = new FontSettings(FontFamily.GenericSansSerif, FontStyle.Regular);
            converter = new WordSizeConverter(fontSettings);
        }

        [Test]
        public void ConvertWeightedWordsCorrectly()
        {
            var words = new[] { new WeightedWord("a", 3)};
            var font = new Font(fontSettings.FontFamily, 3, fontSettings.FontStyle);
            var expected = new[] {new SizedWord("a", font, TextRenderer.MeasureText("a", font))};
            converter.Convert(words).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ConvertWeighted_ThrowArgumentException_WhenEmptyString()
        {
            var words = new[] { new WeightedWord("", 3), new WeightedWord("a", 3)};
            Func<IEnumerable<SizedWord>> action = () => converter.Convert(words);
            action.Enumerating().Should().Throw<ArgumentException>().WithMessage("*?grater than zero");
        }
    }
}
