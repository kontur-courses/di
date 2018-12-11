using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Settings;
using TagsCloudContainer.TextParsers;

namespace TagsCloudContainerTests.TextParserTests
{
    [TestFixture]
    public class TextParserTests
    {
        [Test]
        public void Parse_Should_ReturnWordFrequency()
        {
            var textSettings =new TextSettings(2, new []{"simple"},new string[]{});
            var parser = new TextParser(textSettings);
            var expectation = new List<WordFrequency>
            {
                new WordFrequency("simple", 1),
                new WordFrequency("text", 1)
            };

            var result = parser.Parse($"simple{Environment.NewLine}text");

            result.ShouldBeEquivalentTo(expectation);
        }
    }
}