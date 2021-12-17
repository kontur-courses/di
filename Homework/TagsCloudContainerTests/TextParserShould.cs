using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using TagsCloudContainer.TextParsers;

namespace CloudContainerTests
{
    [TestFixture]
    public class TextParserShould
    {
        private IExcludingWords boringWords = new BoringWords();

        [Test]
        public void Throw_FileNotFoundException_When_Path_Is_Incorrect()
        {
            string incorrectPath = "words";
            Action wordsGetterCreationWithIncorrectPath =
                () => new TextParser(incorrectPath, boringWords, new TxtReader());

            wordsGetterCreationWithIncorrectPath.Should().Throw<FileNotFoundException>();
        }

        [Test]
        public void Throw_ArgumentNullException_When_Path_Is_Null()
        {
            Action wordsGetterCreationWithIncorrectPath =
                () => new TextParser(null, boringWords, new TxtReader());

            wordsGetterCreationWithIncorrectPath.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Show_Distinct_Words_Count_Right()
        {
            var path = "words.txt";
            var wordsCount = 3;
            using (FileStream fs = File.Create(path)) { }
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.WriteLine("only");
                sw.WriteLine("three");
                sw.WriteLine("words");
            }

            var wordsGetter = new TextParser(path, boringWords, new TxtReader());
            var distinctWordsCount = wordsGetter.GetDistinctWordsAmount();

            distinctWordsCount.Should().Be(wordsCount);
            File.Delete(path);
        }
    }
}