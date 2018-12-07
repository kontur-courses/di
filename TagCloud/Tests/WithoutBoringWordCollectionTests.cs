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
        public void GetWords_TextWithBoringWords_ListWithoutBoringWords()
        {
            var textPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Text.txt");
            var words = new LowerWordCollection(
                new WordsFromFile(textPath)
            );
            var boringWords = new WithoutBoringWordCollection(new WordsFromFile(boringWordsPath)
                , words);
            var withoutBoring = boringWords.GetWords();
            withoutBoring.Should().BeEquivalentTo(expectedWords);
        }

        [Test]
        public void DoSomething_WhenSomething()
        {
            var words = new List<string>();
            var boringWords = new WithoutBoringWordCollection(new WordsFromFile(boringWordsPath)
                , new ConstWordCollection(words));
            var withoutBoring = boringWords.GetWords();
            withoutBoring.Should().BeEmpty();
        }
    }
}