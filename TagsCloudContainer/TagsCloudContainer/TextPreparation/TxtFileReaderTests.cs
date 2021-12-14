using System;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.TextPreparation
{
    public class TxtFileReaderTests
    {
        [Test]
        public void GetAllWords_Throws_WhenPathIsNull()
        {
            Action act = () => new TxtFileReader(new WordsReader()).GetAllWords(null);

            act.Should().Throw<ArgumentException>().WithMessage("Path can't be null");
        }

        [Test]
        public void GetAllWords_CallFileReader_WhenCanReadFile()
        {
            var fake = A.Fake<IWordsReader>();
            A.CallTo(() => fake.ReadAllWords(null)).WithAnyArguments().Returns(null);

            new TxtFileReader(fake).GetAllWords("../../input.txt");

            A.CallTo(() => fake.ReadAllWords(null)).WithAnyArguments().MustHaveHappened(1, Times.Exactly);
        }
    }
}