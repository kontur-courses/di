using DeepMorphy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.Enums;

namespace TagCloudVisualizationTests
{
    [TestFixture]
    public class WordPreparatorTests
    {
        [Test]
        public void WordPreparator_ShouldReturnLemmas()
        {
            var wordPreparator = new WordPreparator(new MorphAnalyzer(true));
            var input = new[] { "Овцы", "Бегу", "Весёлые" };

            var actual = wordPreparator.GetPreparedWords(input);

            actual.Should().BeEquivalentTo("овца", "бег", "весёлый");
        }

        [Test]
        public void WordPreparator_ShouldExcludePartsOfSpeech()
        {
            var wordPreparator = new WordPreparator(new MorphAnalyzer(true))
                .Exclude(new[] { SpeechPart.Verb, SpeechPart.Adjective, SpeechPart.AdverbialParticiple });
            var input = new[] { "Овцы", "Бегу", "Весёлые" };

            var actual = wordPreparator.GetPreparedWords(input);

            actual.Should().BeEquivalentTo("овца", "бег");
        }
    }
}