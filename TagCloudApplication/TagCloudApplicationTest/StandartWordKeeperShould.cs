using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagCloudApplication.WordKeepers;

namespace TagCloudApplicationTest
{
    [TestFixture]
    public class StandartWordKeeperShould
    {
        private StandartWordKeeper testWordKeeper;

        [SetUp]
        public void SetUp()
        {
            testWordKeeper = new StandartWordKeeper(new [] {" "});
        }

        [Test]
        [Category("FromString")]
        public void GetWordIncidence_ReturnCorrectWeightedWordList()
        {
            var expected = new List<(string, int)>{("человек",25),("день",25),("рука",25),("работа",25)};
            var text = "человек день рука работа";
            testWordKeeper.GetWordIncidence(text).Should().BeEquivalentTo(expected);

        }

        [Test]
        [Category("FromString")]
        public void GetWordIncidence_CitesWordsInLowerCase()
        {
            var expected = new List<(string, int)> { ("человек", 25), ("день", 25), ("рука", 25), ("работа", 25) };
            var text = "чЕлОвЕк ДеНь РУКА работа";
            testWordKeeper.GetWordIncidence(text).Should().BeEquivalentTo(expected);

        }

        [Test]
        [Category("FromString")]
        public void GetWordIncidence_ReturnsEmptyList_WhenTextIsEmptyString()
        {
            var expected = new List<(string, int)>();
            var text = "";
            testWordKeeper.GetWordIncidence(text).Should().BeEquivalentTo(expected);

        }

        [Test]
        [Category("FromString")]
        public void GetWordIncidence_SetUpRemovingUnnecessaryWordsRule_ByWordFrequency()
        {
            var text = "чЕлОвЕк ДеНь РУКА работа чЕлОвЕк ДеНь РУКА чЕлОвЕк ДеНь РУКА чЕлОвЕк ДеНь РУКА чЕлОвЕк ДеНь РУКА";
            testWordKeeper
                .RemoveUnnecessaryWordsBy(p => p.Frequency < 10)
                .GetWordIncidence(text)
                .Should()
                .BeEquivalentTo(new List<(string, int)> { ("человек", 31), ("день", 31), ("рука", 31) });
        }

        [Test]
        [Category("FromString")]
        public void GetWordIncidence_SetUpRemovingUnnecessaryWordsRule_ByWordForm()
        {
            var text = "чЕлОвЕк ДеНь этот РУКА этоТ чЕлОвЕк" +
                       " эти ДеНь РУКА чЕлОвЕк ДеНь РУКА чЕлОвЕк" +
                       " ДеНь РУКА чЕлОвЕк ДеНь РУКА";
            testWordKeeper
                .RemoveUnnecessaryWordsBy(p => new Regex("эт[аио]т?").IsMatch(p.Word))
                .GetWordIncidence(text)
                .Should()
                .BeEquivalentTo(new List<(string, int)> { ("человек", 27), ("день", 27), ("рука", 27) });
        }

    }
}
