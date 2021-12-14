using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.TextPreparation
{
    public class WordsReaderTests
    {
        [Test]
        public void ReadAllWords_Throws_WhenFileContainsManyWordsInOneLine()
        {
            Action act = () => new WordsReader().ReadAllWords("a b c");

            act.Should().Throw<ArgumentException>().WithMessage("Each line must contain only one word");
        }

        [Test]
        public void ReadAllWords_ReturnsEmptyList_WhenFileIsEmpty()
        {
            new WordsReader().ReadAllWords("").Should().BeEmpty();
        }

        [Test]
        public void ReadAllWords_ReturnsEmptyList_WhenAllLinesAreEmpty()
        {
            new WordsReader().ReadAllWords(Environment.NewLine + Environment.NewLine + Environment.NewLine)
                .Should()
                .BeEmpty();
        }

        [Test]
        public void ReadAllWords_AddsEachWordToResult()
        {
            var expectedResult = new List<string>() {"a", "b", "c"};

            new WordsReader().ReadAllWords("a" + Environment.NewLine + "b" + Environment.NewLine + "c")
                .Should()
                .BeEquivalentTo(expectedResult, options => options.WithStrictOrdering());
        }

        [Test]
        public void ReadAllWords_NotAddsWordsSeparatedByLineBreak()
        {
            var expectedResult = new List<string>() {"a\nb\n"};

            new WordsReader().ReadAllWords("a\nb\n")
                .Should()
                .BeEquivalentTo(expectedResult, options => options.WithStrictOrdering());
        }
    }
}