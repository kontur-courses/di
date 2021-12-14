using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.TextPreparation
{
    public class TxtFileReaderTests
    {
        private TxtFileReader sut;
        private string fileName;

        [SetUp]
        public void InitVariables()
        {
            sut = new TxtFileReader();
            fileName = "a.txt";
        }

        [Test]
        public void GetAllWords_Throws_WhenFileNameIsNull()
        {
            Action act = () => sut.GetAllWords(null);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetAllWords_Throws_WhenFileContainsManyWordsInOneLine()
        {
            System.IO.File.WriteAllText(fileName, "a b c");
            Action act = () => sut.GetAllWords(fileName);

            act.Should().Throw<ArgumentException>().WithMessage("Each line must contain only one word");
            System.IO.File.Delete(fileName);
        }

        [Test]
        public void GetAllWords_ReturnsEmptyList_WhenFileIsEmpty()
        {
            System.IO.File.WriteAllText(fileName, "");

            sut.GetAllWords(fileName).Should().BeEmpty();
        }

        [Test]
        public void GetAllWords_ReturnsEmptyList_WhenAllLinesAreEmpty()
        {
            System.IO.File.WriteAllText(fileName, Environment.NewLine + Environment.NewLine + Environment.NewLine);

            sut.GetAllWords(fileName).Should().BeEmpty();
        }

        [Test]
        public void GetAllWords_AddsEachWordToResult()
        {
            var expectedResult = new List<string>() {"a", "b", "c"};

            System.IO.File.WriteAllText(fileName, "a" + Environment.NewLine + "b" + Environment.NewLine + "c");

            sut.GetAllWords(fileName).Should().BeEquivalentTo(expectedResult);
        }
    }
}