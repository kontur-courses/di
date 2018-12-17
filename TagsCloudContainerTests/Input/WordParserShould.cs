using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Input;
using TagsCloudContainer.Processing.Converting;
using TagsCloudContainer.Processing.Filtering;

namespace TagsCloudContainerTests.Input
{
    [TestFixture]
    public class WordParserShould
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

        [Test, TestCaseSource(nameof(InDefaultStateTestCases))]
        public void InDefaultState(string input, Dictionary<string, int> expected)
        {
            var parser = new WordParser(new IWordFilter[0], new IWordConverter[0]);

            var actual = parser.ParseWords(input);
                
            actual.Should().BeEquivalentTo(expected);
        }

        private static IEnumerable InDefaultStateTestCases()
        {
            yield return new TestCaseData("hello world ha ha", new Dictionary<string, int>
            {
                {"hello", 1}, { "world", 1}, { "ha", 2}
            }).SetName("parse simple text");

            yield return new TestCaseData("hello\r\r\nworld\t\t     \t\r\n\nnew", new Dictionary<string, int>
            {
                {"hello", 1}, { "world", 1}, { "new", 1}
            }).SetName("parse text with whitespaces");

            yield return new TestCaseData("hello, world. That's new me!!", new Dictionary<string, int>
            {
                { "hello", 1}, {"world", 1}, {"new", 1}, {"thats", 1}, {"me", 1}
            }).SetName("parse text with punctuation marks");
        }

        [Test]
        public void ParseTextWithMultipleFilters()
        {
            var filters = new IWordFilter[]
            {
                new BlackListFilter(new[] {"привет", "здорово"}),
                new CommonWordsFilter()
            };
            var parser = new WordParser(filters, new IWordConverter[0]);
            var expected = new Dictionary<string, int>
            {
                { "миша", 1}, {"живется", 1}, {"агрегатор", 1}
            };

            parser.ParseWords("Привет, Миша. Здорово ли тебе живется? Ну ладно, агрегатор!").Should()
                .BeEquivalentTo(expected);
        }
    }
}