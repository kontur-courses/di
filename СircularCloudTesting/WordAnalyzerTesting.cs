using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordProcessing;

namespace СircularCloudTesting
{
    [TestFixture]
    public class WordAnalyzerTesting
    {
        [Test]
        public void MakeWordFrequencyDictionary_Should_CreateDictionaryCorrectly()
        {
            var expectedResult = new Dictionary<string, int>
            {
                {"вырезать",1 },
                {"ракета",1 },
                {"картон",1 },
                {"строгий",1 },
                {"тон",1 },{"вези",1 },{"меня",1 },
                {"сидней",3 }
            };
            var wordAnalyzer = new WordAnalyzer(new WordsSettings() { PathToFile = $"{AppDomain.CurrentDomain.BaseDirectory}/RuDictionary/DefaultTags.txt" });
            var result = wordAnalyzer.MakeWordFrequencyDictionary();
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}