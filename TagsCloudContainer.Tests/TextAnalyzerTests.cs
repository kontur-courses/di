using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Common.Contracts;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class TextAnalyzerTests
    {
        private ITextAnalyzer textAnalyzer;
        private string testDataDir;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            textAnalyzer = ContainerConfig.ConfigureContainer().Resolve<ITextAnalyzer>();
            testDataDir = Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)
                ?.Replace(@"bin\Debug", string.Empty) + @"TestsData\";
        }

        [Test]
        public void GetWordStatisticsFromFile_ShouldWorks_WithTextFiles()
        {
            var path = testDataDir + @"txt\Test_Облако.txt";
            var expected = new KeyValuePair<string, int>("облако", 5);

            var func = new Func<Dictionary<string, int>>(() => textAnalyzer.GetWordStatisticsFromFile(path));
            func.Should().NotThrow();

            func.Invoke().Should().Contain(expected);
        }
        
        [Test]
        public void GetWordStatisticsFromFile_ShouldThrowArgumentException_WithWordFiles()
        {
            var path = testDataDir + @"docx\Test_Облако.docx";
            var func = new Func<Dictionary<string, int>>(() => textAnalyzer.GetWordStatisticsFromFile(path));
            func.Should().Throw<ArgumentException>();
        }

        [TestCase("облако облака\r\nоблаком\tоблаках облаке", "облако", 5,
            TestName = "GetWordStatistics_ShouldStemWords")]
        [TestCase("облако, облака. \"облаком\" - облаках! облаке;", "облако", 5,
            TestName = "GetWordStatistics_ShouldIgnorePunctuationChars")]
        [TestCase("Облако оБлакА облакоМ облаКах ОБЛАКЕ", "облако", 5,
            TestName = "GetWordStatistics_ShouldIgnoreCase")]
        [TestCase("Облако оБлакА облакоМ облаКах ОБЛАКЕ", "облако", 5,
            TestName = "GetWordStatistics_ShouldIgnoreCase")]
        [TestCase("Облако в облаке на облаке за облаком об облаке", "облако", 5,
            TestName = "GetWordStatistics_ShouldIgnorePrepositions")]
        [TestCase("Я Облако Ваше какое-то облако любое облако каким-нибудь облаком неким облаком", "облако", 5,
            TestName = "GetWordStatistics_ShouldIgnorePronouns")]
        public void GetWordStatistics_ShouldWorksCorrectly(string text, string expectedStem, int stemCount)
        {
            var stat = textAnalyzer.GetWordStatistics(text);
            stat.Should().OnlyContain(pair => pair.Key == expectedStem && pair.Value == stemCount);
        }
    }
}