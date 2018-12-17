using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Processing;

namespace TagsCloudContainerTests.Processing
{
    [TestFixture]
    public class PartOfSpeechDetectorShould
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (dir != null)
            {
                Environment.CurrentDirectory = dir;
                Directory.SetCurrentDirectory(dir);
            }
            else
                throw new NullReferenceException("Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) returns null");
        }

        [Test, TestCaseSource(nameof(DetectTestCases))]
        public void Detect(IEnumerable<string> words, Dictionary<string, PartOfSpeech> expected)
        {
            PartOfSpeechDetector.Detect(words).Should().BeEquivalentTo(expected);
        }

        private static IEnumerable DetectTestCases()
        {
            var adjectives = new[] { "замечательный", "благоустроенный" };
            yield return new TestCaseData(adjectives, adjectives.ToDictionary(w => w, w => PartOfSpeech.Adjective))
                .SetName("adjectives");

            var adverbs = new[] { "весело", "по-приятельски" };
            yield return new TestCaseData(adverbs, adverbs.ToDictionary(w => w, w => PartOfSpeech.Adverb))
                .SetName("adverbs");

            var pronouns = new[] { "он", "ему" };
            yield return new TestCaseData(pronouns, pronouns.ToDictionary(w => w, w => PartOfSpeech.Pronoun))
                .SetName("pronouns");

            var numerals = new[] { "первый", "второго" };
            yield return new TestCaseData(numerals, numerals.ToDictionary(w => w, w => PartOfSpeech.Numeral))
                .SetName("numerals");

            var unions = new[] { "и", "а" };
            yield return new TestCaseData(unions, unions.ToDictionary(w => w, w => PartOfSpeech.Union))
                .SetName("unions");

            var interjections = new[] { "ура", "ох" };
            yield return new TestCaseData(interjections, interjections.ToDictionary(w => w, w => PartOfSpeech.Interjection))
                .SetName("interjections");

            var nouns = new[] {"конституций", "работа"};
            yield return new TestCaseData(nouns, nouns.ToDictionary(w => w, w => PartOfSpeech.Noun))
                .SetName("nouns");

            var verbs = new[] {"убежать", "благоухал"};
            yield return new TestCaseData(verbs, verbs.ToDictionary(w => w, w => PartOfSpeech.Verb))
                .SetName("verbs");
        }

        [Test]
        public void SkipInvalidWords()
        {
            var words = new[] {"", null, "привет пока"};
            PartOfSpeechDetector.Detect(words).Should().BeEmpty();
        }
    }
}