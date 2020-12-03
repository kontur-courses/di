using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using WordCloudGenerator;

namespace Tests
{
    public class PreparerTests
    {
        [Test]
        public void GetCountedWords_ShouldReturnCountedWord_OneWordInEachLine()
        {
            var text = "abc\nabc\nefg";
            var preparer = new Preparer(null);

            preparer.CreateWordFreqList(text).Should().BeEquivalentTo(
                new Dictionary<string, int> {["abc"] = 2, ["efg"] = 1});
        }

        [Test]
        public void GetCountedWords_ShouldSkipBoringWords_OneBoringWord()
        {
            var boringWords = new[] {"abc"};
            var text = "abc\nabc\nefg\nefg\nxyz";
            var preparer = new Preparer(boringWords);
            
            preparer.CreateWordFreqList(text).Should().BeEquivalentTo(
                new Dictionary<string, int> {["efg"] = 2, ["xyz"] = 1});
        }
    }
}