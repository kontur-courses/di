using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.WordPrework;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class WordAnalyzerTests
    {
        private readonly WordAnalyzer ordinaryWordAnalyzer = 
            CreateTestWordAnalyzer(new List<string>{ "Стол", "Cтул", "cтул", "Cтул", "коза", "коза",
                "бегать", "прыгать", "мы", "я", "я", "но", "но", "и", "что" });

        private static WordAnalyzer CreateTestWordAnalyzer(IEnumerable<string> words, bool useInfinitiveForm = false)
        {
            var wordGetter = new SimpleWordGetter(words);
            return new WordAnalyzer(wordGetter, useInfinitiveForm);
        }

        [Test]
        public void WordAnalyzer_CountCorrectlyFrequency_OnSimpleInput()
        {
            var analyzer = CreateTestWordAnalyzer(new List<string> { "стол", "стол", "бегать", "бегать", "бегать"});
            var frequency = analyzer.GetWordFrequency();
            frequency.Should().Contain(new KeyValuePair<string, int>("бегать", 3))
                .And.Contain(new KeyValuePair<string, int>("стол", 2));
        }
    }
}