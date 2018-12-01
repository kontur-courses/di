using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsPreprocessors;

namespace TagsCloudContainer.Tests
{
    public class WordsLowerTests
    {
        [Test]
        public void Preprocess_LowersWords()
        {
            var words = new List<string>()
            {
                "ПРИВЕТ",
                "Я",
                "ПИШУ",
                "ТеСтЫ",
                "нА",
                "ЗаНИЖАТЕЛЬ",
                "СЛОВ",
                "",
            };

            var wordsLower = new WordsLower();

            var result = wordsLower.Preprocess(words);

            result.Should()
                .BeEquivalentTo(words.Select(z => z.ToLower(CultureInfo.InvariantCulture)));
        }
    }
}