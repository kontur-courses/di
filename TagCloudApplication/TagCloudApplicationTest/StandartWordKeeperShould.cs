using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;
using TagCloudApplication.Readers;
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
            testWordKeeper = new StandartWordKeeper(new [] {" "}, new TXTReader());
        }

        [Test]
        public void GetWordIncidence_ReturnCorrectWeightedWordList()
        {
            var fileName = "Test1.txt";
            CreateNewTestFile("человек день рука работа", fileName);
                        
            testWordKeeper.GetWordIncidence(fileName).Should().BeEquivalentTo(new List<(string, int)>
                { ("человек", 25), ("день", 25), ("рука", 25), ("работа", 25) });

        }

        [Test]
        public void GetWordIncidence_CitesWordsInLowerCase()
        {
            var fileName = "Test2.txt";
            CreateNewTestFile("чЕлОвЕк ДеНь РУКА работа", fileName);

            testWordKeeper.GetWordIncidence(fileName).Should().BeEquivalentTo(new List<(string, int)>
                { ("человек", 25), ("день", 25), ("рука", 25), ("работа", 25) });

        }

        [Test]
        public void GetWordIncidence_ReturnsEmptyList_WhenTextIsEmptyString()
        {
            var fileName = "Test3.txt";
            CreateNewTestFile("", fileName);

            testWordKeeper.GetWordIncidence(fileName).Should().BeEquivalentTo(new List<(string, int)>());

        }

        [Test]
        public void GetWordIncidence_SetUpRemovingUnnecessaryWordsRule_ByWordFrequency()
        {
            var fileName = "Test4.txt";
            CreateNewTestFile("чЕлОвЕк ДеНь РУКА работа чЕлОвЕк ДеНь РУКА чЕлОвЕк ДеНь РУКА чЕлОвЕк ДеНь РУКА чЕлОвЕк ДеНь РУКА", fileName);

            testWordKeeper
                .RemoveUnnecessaryWordsBy(p => p.Frequency < 10)
                .GetWordIncidence(fileName)
                .Should()
                .BeEquivalentTo(new List<(string, int)> { ("человек", 31), ("день", 31), ("рука", 31) });
        }

        [Test]
        public void GetWordIncidence_SetUpRemovingUnnecessaryWordsRule_ByWordForm()
        {
            var text = "чЕлОвЕк ДеНь этот РУКА этоТ чЕлОвЕк" +
                       " эти ДеНь РУКА чЕлОвЕк ДеНь РУКА чЕлОвЕк" +
                       " ДеНь РУКА чЕлОвЕк ДеНь РУКА";
            var fileName = "Test4.txt";

            CreateNewTestFile(text, fileName);

            testWordKeeper
                .RemoveUnnecessaryWordsBy(p => new Regex("эт[аио]т?").IsMatch(p.Word))
                .GetWordIncidence(fileName)
                .Should()
                .BeEquivalentTo(new List<(string, int)> { ("человек", 27), ("день", 27), ("рука", 27) });
        }


        private void CreateNewTestFile(string fileContent, string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                var data = Encoding.Default.GetBytes(fileContent);
                fs.Write(data, 0, data.Length);
            }
        }
    }
}
