using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.WordPrework;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class WordAnalyzerTests
    {

        private static WordAnalyzer CreateTestWordAnalyzer(IEnumerable<string> words, bool useInfinitiveForm = false)
        {
            var wordGetter = new SimpleWordGetter(words);
            return new WordAnalyzer(wordGetter, useInfinitiveForm);
        }

        [Test]
        public void GetWordFrequency_CountCorrectlyFrequency_OnSimpleInput()
        {
            var analyzer = CreateTestWordAnalyzer(new List<string> { "стол", "стол", "бегать", "бегать", "бегать"});
            var frequency = analyzer.GetWordFrequency();
            frequency.Should().HaveCount(2)
                .And.Contain(new KeyValuePair<string, int>("бегать", 3))
                .And.Contain(new KeyValuePair<string, int>("стол", 2));
        }

        [Test]
        public void GetWordFrequency_IgnoresBoringPartsOfSpeech_OnDefaultpartOfSpeech()
        {
            var analyzer = CreateTestWordAnalyzer(new List<string> { "стол", "стол", "и", "но", "что", "я", "мы" });
            var frequency = analyzer.GetWordFrequency();
            frequency.Should().HaveCount(1)
                .And.Contain(new KeyValuePair<string, int>("стол", 2));
        }

        [Test]
        public void GetWordFrequency_IgnoresBoringPartsOfSpeech_OnDefinedPartsOfSpeech()
        {
            var analyzer = CreateTestWordAnalyzer(new List<string> { "стол", "бегать", "и", "я", "я"});
            var frequency = analyzer.GetWordFrequency(new HashSet<PartOfSpeech>{PartOfSpeech.Noun, PartOfSpeech.Verb});
            frequency.Should().HaveCount(2)
                .And.Contain(new KeyValuePair<string, int>("я", 2))
                .And.Contain(new KeyValuePair<string, int>("и", 1));
        }

        [Test]
        public void GetWordFrequency_ReturnsInfinitive_OnAppropriateParameter()
        {
            var analyzer = CreateTestWordAnalyzer(new List<string>
                { "стол", "столы", "стола", "бегала", ",бегал", ",бегали"}, true);
            var frequency = analyzer.GetWordFrequency();
            frequency.Should().HaveCount(2)
                .And.Contain(new KeyValuePair<string, int>("стол", 3))
                .And.Contain(new KeyValuePair<string, int>("бегать", 3));
        }

        [Test]
        public void GetSpecificWordFrequency_CountCorrectlyFrequency_OnSinglePartOfSpeech()
        {
            var analyzer = CreateTestWordAnalyzer(new List<string> { "стол", "стол", "стул", "стул",
                "стул", "бегать", "бегать", "я", "и"});
            var frequency = analyzer.GetSpecificWordFrequency(new List<PartOfSpeech> {PartOfSpeech.Noun});
            frequency.Should().HaveCount(2)
                .And.Contain(new KeyValuePair<string, int>("стул", 3))
                .And.Contain(new KeyValuePair<string, int>("стол", 2));
        }

        [Test]
        public void GetSpecificWordFrequency_CountCorrectlyFrequency_OnMultiplePartOfSpeech()
        {
            var analyzer = CreateTestWordAnalyzer(new List<string> { "стол", "стол", "стул", "стул",
                "стул", "бегать","бегать", "я", "и"});
            var frequency = analyzer.GetSpecificWordFrequency(new List<PartOfSpeech> { PartOfSpeech.Noun, PartOfSpeech.Verb });
            frequency.Should().HaveCount(3)
                .And.Contain(new KeyValuePair<string, int>("стул", 3))
                .And.Contain(new KeyValuePair<string, int>("стол", 2))
                .And.Contain(new KeyValuePair<string, int>("бегать", 2));
        }

        [Test]
        public void GetSpecificWordFrequency_ReturnsInfinitive_OnAppropriateParameter()
        {
            var analyzer = CreateTestWordAnalyzer(new List<string>
                { "стол", "столы", "стола", "бегала", ",бегал", ",бегали", "я" ,"мы"}, true);
            var frequency = analyzer.GetSpecificWordFrequency(new List<PartOfSpeech>
                {PartOfSpeech.Noun, PartOfSpeech.Verb});
            frequency.Should().HaveCount(2)
                .And.Contain(new KeyValuePair<string, int>("стол", 3))
                .And.Contain(new KeyValuePair<string, int>("бегать", 3));
        }

    }
}