using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TagsCloudContainer;

namespace TagsCloudContainerTest
{
    public class ProcessorNonBoringWordsToLowerTest
    {
        private ProcessorNonBoringWordsToLower processor;

        [SetUp]
        public void InitializeProcessor()
        {
            var boringWords = new HashSet<string>() {"А", "б", "в"};
            processor = new ProcessorNonBoringWordsToLower(boringWords);
        }

        [Test]
        public void CheckToLowerFunc()
        {
            var input = new List<string>() { "ASdsaASD" };

            var output = processor.Process(input);

            output.First().Should().Be(input.First().ToLower());
        }

        [Test]
        public void ExcludeBoringWordInLower()
        {
            var input = new List<string>() { "Б" };

            var output = processor.Process(input);

            output.Should().BeEmpty();
        }

        [Test]
        public void ExcludeBoringWord()
        {
            var input = new List<string>() { "б" };

            var output = processor.Process(input);

            output.Should().BeEmpty();
        }
    }
}
