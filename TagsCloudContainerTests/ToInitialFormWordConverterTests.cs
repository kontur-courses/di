using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CommandsExecuting;
using TagsCloudContainer.MyStem;
using TagsCloudContainer.WordProcessing.Converting;

namespace TagsCloudContainerTests
{
    public class ToInitialFormWordConverterTests
    {
        private ToInitialFormWordConverter wordConverter;

        [SetUp]
        public void SetUp()
        {
            var pathToMyStemDirectory = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent?.Parent?.FullName,
                "TagsCloudContainer", "MyStem");
            wordConverter = new ToInitialFormWordConverter(
                new MyStemExecutor(new CmdCommandExecutor(), pathToMyStemDirectory), new MyStemResultParser());
        }


        [TestCaseSource(nameof(ConvertWordsShouldReturnUnchangedWhenInputInInitialFormTestCases))]
        public void ConvertWords_ShouldReturnUnchanged_WhenInputInInitialForm(List<string> words)
        {
            var convertedWords = wordConverter.ConvertWords(words);

            convertedWords.Should().BeEquivalentTo(words);
        }

        private static IEnumerable ConvertWordsShouldReturnUnchangedWhenInputInInitialFormTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string> {"мир", "труд", "май"}).SetName("nouns in initial form");
                yield return new TestCaseData(new List<string> {"прекрасный", "день", "быть", "вчера"}).SetName(
                    "different parts of speech in initial form");
                yield return new TestCaseData(new List<string> {"к", "не", "красиво", "еще", "и"}).SetName(
                    "forever unchanged words");
            }
        }

        [Test]
        public void ConvertWords_ShouldReturnConverted_WhenInputNotInInitialCase()
        {
            var words = new[] {"прекрасного", "дня", "хорошего", "вечера", "вечером"};

            var convertedWords = wordConverter.ConvertWords(words);

            var expectedConvertedWords = new[] {"прекрасный", "день", "хороший", "вечер", "вечер"};
            convertedWords.Should().BeEquivalentTo(expectedConvertedWords);
        }

        [TestCaseSource(nameof(ConvertWordsShouldConvertKindTestCases))]
        public List<string> ConvertWords_ShouldConvertKind(List<string> words)
        {
            var convertedWords = wordConverter.ConvertWords(words);

            return convertedWords.ToList();
        }

        private static IEnumerable ConvertWordsShouldConvertKindTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string>
                        {"прекрасная", "хорошей", "медленной", "красивое", "любого"}).SetName("adjectives in input")
                    .Returns(new List<string> {"прекрасный", "хороший", "медленный", "красивый", "любой"});
                yield return new TestCaseData(new List<string> {"жила", "была", "дышало", "двигалось"})
                    .SetName("verbs in input").Returns(new List<string> {"жить", "быть", "дышать", "двигаться"});
            }
        }

        [Test]
        public void ConvertWords_ShouldConvertTime_WhenVerbsInInput()
        {
            var words = new[] {"был", "жил", "сделаю", "спать", "отдохну"};

            var convertedWords = wordConverter.ConvertWords(words);

            var expectedConvertedWords = new[] { "быть", "жить", "сделать", "спать", "отдыхать" };
            convertedWords.Should().BeEquivalentTo(expectedConvertedWords);
        }
    }
}