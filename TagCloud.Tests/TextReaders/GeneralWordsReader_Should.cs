using System;
using FluentAssertions;
using FakeItEasy;
using NUnit.Framework;
using TagCloud.Core.TextWorking.WordsReading;
using TagCloud.Core.TextWorking.WordsReading.WordsReadersForFiles;

namespace TagCloud.Tests.TextReaders
{
    [TestFixture]
    public class GeneralWordsReader_Should : AbstractWordsReader_Should<GeneralWordsReader>
    {
        private GeneralWordsReader generalWordsReader;
        private IWordsReaderForFile ext1WordsReader;
        private IWordsReaderForFile ext2WordsReader;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ext1WordsReader = A.Fake<IWordsReaderForFile>();
            A.CallTo(() => ext1WordsReader.ReadingFileExtension).Returns(".ext1");

            ext2WordsReader = A.Fake<IWordsReaderForFile>();
            A.CallTo(() => ext2WordsReader.ReadingFileExtension).Returns(".ext2");

            reader = new GeneralWordsReader(new[] {ext1WordsReader, ext2WordsReader});
            generalWordsReader = reader;
        }

        [Test]
        public void UseDifferentInnerReaders_OnFilesWithDifferentFormats()
        {
            const string pathForExt1 = "my_fake_words.ext1";
            const string pathForExt2 = "my_fake_words.ext2";

            generalWordsReader.ReadFrom(pathForExt1);
            generalWordsReader.ReadFrom(pathForExt2);

            A.CallTo(() => ext1WordsReader.ReadFrom(pathForExt1)).MustHaveHappenedOnceExactly();
            A.CallTo(() => ext2WordsReader.ReadFrom(pathForExt2)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ThrowArgumentException_WhenCantFindReaderForParticularFileExtension()
        {
            Action readingAction = () => generalWordsReader.ReadFrom("file.with_wrong_format");
            readingAction.Should().Throw<ArgumentException>();
        }
    }
}