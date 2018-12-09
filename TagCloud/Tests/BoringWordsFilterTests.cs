using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloud.Tests
{
    public class WithoutBoringWordCollectionTests
    {
        private readonly string boringWordsPath =
            Path.Combine(TestContext.CurrentContext.TestDirectory, "BoringWords.txt");

        private readonly List<string> expectedWords = new List<string>
        {
            "neural",
            "networks",
            "only",
            "machine",
            "learning",
            "frameworks"
        };

        [Test]
        public void DeleteBoringWords_TextWithBoringWords_ListWithoutBoringWords()
        {
            var textPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Text.txt");
            var words = new LowerWord(new WordsFromFile(textPath));
            var boringWords = new WordsFromFile(boringWordsPath).GetWords();
            var boringWordsFilter = new BoringWordsFilter(boringWords, words.ToLower());
            var withoutBoring = boringWordsFilter.DeleteBoringWords();
            withoutBoring.Should().BeEquivalentTo(expectedWords);
        }

        [Test]
        public void DeleteBoringWords_EmptyText()
        {
            var words = new List<string>();
            var boringWords = new BoringWordsFilter(new WordsFromFile(boringWordsPath).GetWords()
                , new ConstWordCollection(words).DeleteBoringWords());
            var withoutBoring = boringWords.DeleteBoringWords();
            withoutBoring.Should().BeEmpty();
        }
    }
}