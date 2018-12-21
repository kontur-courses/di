using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TagsCloudContainer.ColorAlgorithm;

namespace TagsCloudContainerTests
{
    [TestFixture()]
    class ColorsAlgorithmsTests
    {
        [Test]
        public void RandomAlgorithm_Should_GenerateValidColor()
        {
            var algorithm = new RandomColorAlgorithm();
            Action action = () => algorithm.GetColor();

            action.Should().NotThrow<ArgumentException>();
        }

        [Test]
        public void StaticAlgorithm_Should_ReturnBlackColor()
        {
            var algorithm = new StaticColorAlgorithm();

            algorithm.GetColor().Should().BeEquivalentTo(Color.Black);
        }

        [Test]
        public void FrequencyAlgorithm_Should_ReturnBlack_NoWords()
        {
            var algorithm = new FrequencyColorAlgorithm();

            algorithm.GetColor().Should().BeEquivalentTo(Color.Black);
        }

        [Test]
        public void FrequencyAlgorithm_Should_ReturnBlack_NoCurrentWordInDictionary()
        {
            var algorithm = new FrequencyColorAlgorithm();

            algorithm.GetColor(new Dictionary<string, int>(), "aaa").Should().BeEquivalentTo(Color.Black);
        }

        [Test]
        public void FrequencyAlgorithm_Should_ReturnBlack_OneWordInDictionary()
        {
            var algorithm = new FrequencyColorAlgorithm();
            var words = new Dictionary<string, int>()
            {
                {"aaa", 1}
            };

            algorithm.GetColor(words, "aaa").Should().BeEquivalentTo(Color.FromArgb(0, 0, 0));
        }

        [Test]
        public void FrequencyAlgorithm_Should_ReturnGray_TwoWordsInDictionary()
        {
            var algorithm = new FrequencyColorAlgorithm();
            var words = new Dictionary<string, int>()
            {
                {"aaa", 1},
                {"aab", 2 }
            };
            var hue = 255 - (int)(255 / 2);

            algorithm.GetColor(words, "aaa").Should().BeEquivalentTo(Color.FromArgb(hue, hue, hue));
        }
    }
}
