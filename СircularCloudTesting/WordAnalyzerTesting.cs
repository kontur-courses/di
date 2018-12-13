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

        [TestCase("testDocFile.doc", TestName = "File has an extension doc")]
        [TestCase("testDocxFile.docx", TestName = "File has an extension docx")]
        [TestCase("testTxtFile.txt", TestName = "File has an extension txt")]
        public void MakeWordFrequencyDictionary_Should_ProcessFileCorrectly_When(string fileName)
        {
            var wordAnalyzer = new WordAnalyzer(new WordsSettings()
            { PathToFile = $"{AppDomain.CurrentDomain.BaseDirectory}/TestingFiles/" + fileName });
            var result = wordAnalyzer.MakeWordFrequencyDictionary();
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}