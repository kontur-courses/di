using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TextPreprocessor.Core;
using TextPreprocessor.TextAnalyzers;
using TextPreprocessor.TextRiders;

namespace TextPreprocessor.Tests
{
    [TestFixture]
    public class FrequencyTextAnalyzerTests
    {
        private IEnumerable<Tag> tags = new[]
        {
            new Tag("every"),
            new Tag("time"),
            new Tag("time"),
            new Tag("time"),
            new Tag("after"),
            new Tag("every"),
            new Tag("hit"),
            new Tag("take"),
        };

        private ITextAnalyzer textAnalyzer;
        
        [SetUp]
        public void SetUp()
        {
            textAnalyzer = new FrequencyTextAnalyzer();
        }

        [Test]
        public void MustCorrectlyCountNumberIdenticalWords()
        {
            var tagInfos = textAnalyzer.GetTagInfo(tags)
                .ToDictionary(tagInfo => tagInfo.Tag.Content, tagInfo =>  tagInfo.Frequency);

            tagInfos["every"].Should().Be(2);
            tagInfos["time"].Should().Be(3);
            tagInfos["after"].Should().Be(1);
            tagInfos["hit"].Should().Be(1);
            tagInfos["take"].Should().Be(1);
        }
    }
}