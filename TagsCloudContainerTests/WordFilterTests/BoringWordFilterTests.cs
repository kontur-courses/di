using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordFilter;

namespace TagsCloudContainerTests.WordFilterTests
{
    [TestFixture]
    public class BoringWordFilterTests
    {
        [Test]
        public void BoringWord_ShouldSkipBoringWords()
        {
            var boringWords = new List<string>{"привет" , "стул"};   
            var filter = new BoringWordFilter(boringWords);
            var word = "привет";

            filter.Validate(word).Should().BeFalse();
        }
    }
}