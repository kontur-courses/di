using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloudCreator;

namespace TagCloudTests
{
    public class WordPrepairerTests
    {
        [Test]
        public void GetInterestingWords_NounsInDifferentForm_NounsInDefaultForm()
        {
            var returnedWords =
                WordPrepairer.GetInterestingWords(new[] {"инженеры", "коты", "ИнЖеНЕров", "Кота", "о коте"});
            returnedWords.All(x => x == "инженер" || x == "кот").Should().BeTrue();
            returnedWords.Length.Should().Be(5);
        }

        [Test]
        public void GetInterestingWords_BoringWords_EmptyArray()
        {
            WordPrepairer.GetInterestingWords("а но ты я оно нет да это то для когда и".Split(' ')).Length.Should()
                .Be(0);
        }

        [Timeout(2000)]
        [Test]
        public void GetInterestingWords_OneThousandWords_WorkFast()
        {
            WordPrepairer.GetInterestingWords(Enumerable.Range(0, 1000).Select(x => "пакетов").ToArray());
        }

        [Test]
        public void GetWordsStatistic_DifferentWords_CorrectStatistic()
        {
            var words =
                ("даже плохой программный код может работать однако если код не является чистым " +
                 "это всегда будет мешать развитию проекта и компании разработчика " +
                 "отнимая значительные ресурсы на его поддержку и укрощение")
                .Split(' ');
            var statistic = WordPrepairer.GetWordsStatistic(words);
            statistic.Count(x => x.Item1 == "код").Should().Be(1);
            statistic.First(x => x.Item1 == "код").Item2.Should().Be(2);
        }

        [Test]
        public void GetWordsStatistic_EmptyArray_EmptyList()
        {
            WordPrepairer.GetWordsStatistic(new string[0]).Count.Should().Be(0);
        }

        [Test]
        public void GetWordsStatistic_Null_ThrowsException()
        {
            new Action(() => WordPrepairer.GetWordsStatistic(null)).Should().Throw<NullReferenceException>();
        }
    }
}