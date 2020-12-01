using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainerTests
{
    public class TextAnalyzerShould
    {
        [TestCase(null, TestName = "When string is null")]
        [TestCase("", TestName = "When string is empty")]
        [TestCase("твой ваш он я ты в над под, ах ура",
            TestName = "When string contains only boring words: pronouns, prepositions, interjections")]
        public void GetInterestingWords_ThrowException(string text)
        {
            var act = new Action(() => new TextAnalyzer().GetInterestingWords(text, new string [0]));

            act.Should().Throw<Exception>();
        }

        [TestCase("ученик, cтудент, первый, второй, красивый, неожиданно, ,бегать",
            TestName = "Interesting words: nouns, adverbs, verbs, adjectives, numerals")]
        public void GetInterestingWords_NotException_WhenContainsInterestingWords(string text)
        {
            var act = new Action(() => new TextAnalyzer().GetInterestingWords(text, new string [0]));

            act.Should().NotThrow<Exception>();
        }

        [TestCase("ученик, cтудент, первый, второй, красивый, неожиданно, ,бегать, твой, я ты",
            TestName = "Interesting words: nouns, adverbs, verbs, adjectives, numerals")]
        public void GetInterestingWords_InterestingWordsInTheInitialForm_WhenContainsInterestingWords(string text)
        {
            var act = new TextAnalyzer().GetInterestingWords(text, new string [0]);

            act.Should().BeEquivalentTo("ученик", "студент", "первый", "второй", "красивый", "неожиданно", "бегать");
        }
    }
}