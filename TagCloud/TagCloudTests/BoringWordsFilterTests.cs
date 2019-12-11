using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud;

namespace TagCloudTests
{
    public class BoringWordsFilterTests
    {
        private BoringWordsFilter boringWordsFilter;
        private BoringWord[] boringWords = 
        {
            new BoringWord("a"),
            new BoringWord("on"),
            new BoringWord("the"),
            new BoringWord("in")
        };
        private string[] allWords =
        {
            "josh",
            "on",
            "a",
            "bike",
            "crushed",
            "in",
            "the",
            "park"
        };

        [SetUp]
        public void BaseSetUp()
        {
            boringWordsFilter = new BoringWordsFilter(boringWords);
        }

        [Test]
        public void BoringWordsFilterShould_ReturnAllWords_OnEmptyBoringWords()
        {
            boringWordsFilter = new BoringWordsFilter(new BoringWord[] { });
            boringWordsFilter.FilterWords(allWords).Length.Should().Be(allWords.Length);
        }

        [Test]
        public void BoringWordsFilterShould_NoWords_OnEmptyWords()
        {
            boringWordsFilter.FilterWords(new string[] { }).Length.Should().Be(0);
        }

        [Test]
        public void BoringWordsFilterShould_FilterdWords()
        {
            boringWordsFilter.FilterWords(allWords).Length.Should().Be(4);
        }
    }
}
