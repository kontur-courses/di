using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.DefinerFontSize;
using TagsCloudVisualization.Infrastructure.Analyzer;
using TagsCloudVisualization.Settings;

namespace TagsCloudContainerTests
{
    public class DefinerFontSizeShould
    {
        private DefinerFontSize definer;

        private FontSettings settings;

        [SetUp]
        public void SetUp()
        {
            settings = new FontSettings();
            definer = new DefinerFontSize(settings);
        }

        [Test]
        public void ReturnEmptyWhenGivenEmpty()
        {
            var result = definer
                .DefineFontSize(Enumerable.Empty<IWeightedWord>())
                .ToArray();

            result.Should().BeEmpty();
        }


        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public void ReturnsEqualNumberWords(int count)
        {
            var words = Enumerable.Repeat(new WeightedWord { Weight = 1, Word = "hello" }, count);
            settings.MaxEmSize = 100;


            var result = definer.DefineFontSize(words);

            result.Count().Should().Be(count);
        }

        [Test]
        public void WhenReceivingOneWord_MaximumSizeSet()
        {
            var words = new[]
            {
                new WeightedWord { Weight = 1, Word = "hello" }
            };
            settings.MaxEmSize = 100;

            var result = definer.DefineFontSize(words);

            result.First().Font.Size.Should().BeInRange(settings.MaxEmSize - 1e-4f,
                settings.MaxEmSize + 1e-4f);
        }

        [Test]
        public void CalculatedFromMaxSize()
        {
            var weights = new[] { 5, 3, 1 };

            var words = new[]
            {
                new WeightedWord { Weight = 5, Word = "hello" },
                new WeightedWord { Weight = 3, Word = "hello" },
                new WeightedWord { Weight = 1, Word = "hello" }
            };
            settings.MaxEmSize = 100;
            settings.MinEmSize = 0;
            var result = definer.DefineFontSize(words).ToArray();

            for (var i = 0; i < result.Length; i++)
                result[i].Font.Size.Should().BeInRange(settings.MaxEmSize * weights[i] / weights[0] - 1e-4f,
                    settings.MaxEmSize * weights[i] / weights[0] + 1e-4f);
        }
    }
}