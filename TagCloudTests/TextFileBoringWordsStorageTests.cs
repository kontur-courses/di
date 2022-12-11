using FluentAssertions;
using NUnit.Framework;
using TagCloud.BoringWordsRepositories;
using TagCloud.Readers;

namespace TagCloudTests
{
    public class TextFileBoringWordsStorageTests
    {
        private IBoringWordsStorage boringWordsStorage;
        [SetUp]
        public void CreateTextFileBoringWordsStorageTests()
        {
            boringWordsStorage = new TextFileBoringWordsStorage(new SingleWordInRowTextFileReader());
        }

        [Test]
        public void TextFileBoringWordsStorage_GetBoringWords_ShouldReturnedEmptyCollectionAfterCtor()
        {
            boringWordsStorage.GetBoringWords().Should().BeEmpty();
        }

        [TestCase("empty.txt", TestName = "file without words")]
        public void TextFileBoringWordsStorage_GetBoringWords_ShouldReturnedEmptyCollectionWhen(string path)
        {
            boringWordsStorage.LoadBoringWords(path);
            boringWordsStorage.GetBoringWords().Should().BeEmpty();
        }

        [TestCase("BoringWordsDictionary.txt", TestName = "file with words")]
        public void TextFileBoringWordsStorage_GetBoringWords_ShouldReturnedCollectionWhen(string path)
        {
            boringWordsStorage.LoadBoringWords(path);
            boringWordsStorage.GetBoringWords().Should().HaveCountGreaterThan(1);
        }
    }
}
