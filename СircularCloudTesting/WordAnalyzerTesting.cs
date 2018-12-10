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
        private Dictionary<string, int> expectedResult;

        [SetUp]
        public void Init()
        {
            expectedResult = expectedResult = new Dictionary<string, int>
            {
                {"вырезать",1 },{"ракета",1 }, {"картон",1 },{"строгий",1 },
                {"тон",1 }, {"произнёс",1},{"вези",1 },{"меня",1 }, {"сидней",3 }
            };
        }

        [Test]
        public void MakeWordFrequencyDictionary_Should_ProcessDocFileCorrectly()
        {
            var wordAnalyzer = new WordAnalyzer(new WordsSettings()
                { PathToFile = $"{AppDomain.CurrentDomain.BaseDirectory}/TestingFiles/testDocFile.doc" });
            var result = wordAnalyzer.MakeWordFrequencyDictionary();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void MakeWordFrequencyDictionary_Should_ProcessDocxFileCorrectly()
        {
            var wordAnalyzer = new WordAnalyzer(new WordsSettings()
                { PathToFile = $"{AppDomain.CurrentDomain.BaseDirectory}/TestingFiles/testDocxFile.docx" });
            var result = wordAnalyzer.MakeWordFrequencyDictionary();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void MakeWordFrequencyDictionary_Should_ProcessTxtFileCorrectly()
        {
            var wordAnalyzer = new WordAnalyzer(new WordsSettings()
                { PathToFile = $"{AppDomain.CurrentDomain.BaseDirectory}/TestingFiles/testTxtFile.txt" });
            var result = wordAnalyzer.MakeWordFrequencyDictionary();
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}