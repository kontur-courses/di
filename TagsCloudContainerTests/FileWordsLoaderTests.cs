using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class FileWordsLoaderTests
    {
        [Test]
        public void Ctor_IfFileFromPathDoesNotExist_ThrowArgumentException()
        {
            Action act = () => new FileWordsLoader("doesn't exist");
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Ctor_IfFileExtensionIsNotSupported_ThrowArgumentException()
        {
            Action act = () => new FileWordsLoader("../../../TestFiles/test.rtf");
            act.Should().Throw<ArgumentException>();
        }

        [TestCase("../../../TestFiles/test.txt", TestName = "WithTxtFile")]
        [TestCase("../../../TestFiles/test.docx", TestName = "WithDocxFile")]
        public void GetWords_WorkWithDifferentFormatOfFiles(string path)
        {
            var loader = new FileWordsLoader(path);

            loader.GetWords().Should().BeEquivalentTo(
                new[] {"1", "2", "3"},
                options => options.WithStrictOrdering());
        }
    }
}