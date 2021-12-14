using System;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.TextPreparation
{
    public class DocFileReaderTests
    {
        [Test]
        public void GetAllWords_Throws_WhenPathIsNull()
        {
            Action act = () => new DocFileReader(new WordsReader()).GetAllWords(null);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetAllWords_CallFileReader_WhenCanReadFile()
        {
            var fake = A.Fake<IWordsReader>();
            A.CallTo(() => fake.ReadAllWords(null)).WithAnyArguments().Returns(null);

            new DocFileReader(fake).GetAllWords("../../input.docx");

            A.CallTo(() => fake.ReadAllWords(null)).WithAnyArguments().MustHaveHappened(1, Times.Exactly);
        }

        [Test]
        public void GetAllWords_Throws_WhenCantReadFile()
        {
            Action act = () => new DocFileReader(new WordsReader()).GetAllWords("../../input.txt");

            act.Should().Throw<Exception>();
        }
    }
}