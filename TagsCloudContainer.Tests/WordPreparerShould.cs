using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.WordPreparers;

namespace TagsCloudContainer.Tests.WordPreparerTests
{
    [TestFixture]
    internal class WordPreparerShould
    {
        private IWordPreparer wordPreparer;

        [SetUp]
        public void SetUp()
        {
            wordPreparer = WordPreparerFactory.CreateDefault();
        }

        [Test]
        public void ThrowArgumentException_WhenWordsAreNull()
        {
            Action action = () => wordPreparer.Prepare(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ReturnEmptyArray_WhenInputIsEmpty()
        {
            var words = Array.Empty<string>();

            var actual = wordPreparer.Prepare(words);

            actual.Should().BeEmpty();
        }

        [Test]
        public void ReturnSameAsInput_WhenInputIsInLowerCase()
        {
            var words = new string[] { "some", "text" };

            var actual = wordPreparer.Prepare(words);

            actual.Should().BeEquivalentTo(words);
        }

        [Test]
        public void TurnUpperCaseToLowerCase()
        {
            var words = new string[] { "UPPER", "Case", "case" };
            var expected = new string[] { "upper", "case", "case" };

            var actual = wordPreparer.Prepare(words);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ExcludeAll_ExceptVerbNounAndAdjective()
        {
            wordPreparer = WordPreparerFactory.CreateDefault(new WordType[] { WordType.Other });
            var words = new string[] { "take", "noun", "this", "beautiful" };
            var expected = new string[] { "take", "noun", "beautiful" };

            var actual = wordPreparer.Prepare(words);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ExcludeVerbAndNoun()
        {
            wordPreparer = WordPreparerFactory.CreateDefault(new WordType[] { WordType.Verb, WordType.Noun });
            var words = new string[] { "take", "noun", "this", "beautiful" };
            var expected = new string[] { "this", "beautiful" };

            var actual = wordPreparer.Prepare(words);

            actual.Should().BeEquivalentTo(expected);
        }
    }

    internal static class WordPreparerFactory
    {
        public static IWordPreparer CreateDefault() => new WordPreparer(Array.Empty<WordType>());
        public static IWordPreparer CreateDefault(WordType[] excludedTypes) => new WordPreparer(excludedTypes);
    }
}