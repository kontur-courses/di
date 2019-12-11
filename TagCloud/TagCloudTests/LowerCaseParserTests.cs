using NUnit.Framework;
using System;
using TagCloud;

namespace TagCloudTests
{
    public class LowerCaseParserTests
    {
        private LowerCaseParser lowerCaseParser;

        [SetUp]
        public void BaseSetUp()
        {
            lowerCaseParser = new LowerCaseParser();
        }

        [TestCase(new object[] { "" }, ExpectedResult = new string[] { "" })]
        [TestCase(new object[] { "a" }, ExpectedResult = new string[] { "a" })]
        [TestCase(new object[] { "A" }, ExpectedResult = new string[] { "a" })]
        [TestCase(new object[] { "AaA", "AAA" }, ExpectedResult = new string[] { "aaa", "aaa" })]
        public string[] LowerCaseParserShould_ReturnCorrectParsedWords(params string[] words)
        {
            return lowerCaseParser.ParseWords(words);
        }
    }
}
