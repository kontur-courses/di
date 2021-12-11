#region

using System;
using DeepMorphy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.Enums;

#endregion

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

        [Test]
        public void WordPreparator_ShouldReturnEmptyCollection_WhenInputIsEmpty()
        {
            var wordPreparator = new WordPreparator(new MorphAnalyzer(true));

            var actual = wordPreparator.GetPreparedWords(Array.Empty<string>());

            actual.Should().BeEmpty();
        }

        [Test]
        public void WordPreparator_ShouldThrowArgumentException_WhenInputWasNull()
        {
            var wordPreparator = new WordPreparator(new MorphAnalyzer(true));

            Action act = () => wordPreparator.GetPreparedWords(null);

            act.Should().Throw<ArgumentException>();
        }
    }
}