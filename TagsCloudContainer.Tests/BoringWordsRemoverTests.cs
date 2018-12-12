using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsPreprocessors;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class BoringWordsRemoverTests
    {
        private IWordsPreprocessor boringWordsRemover;

        [SetUp]
        public void DoBeforeAnyTest()
        {
            boringWordsRemover = new BoringWordsRemover();
        }

        [Test]
        public void Preprocess_RemovesBoringWords()
        {
            var data = "Заходи в дом. Иди на ту сторону".ToLower();
            var expected = "Заходи дом иди сторону".ToLower()
                .Split();

            var actual = boringWordsRemover.Preprocess(data.Split());

            actual.Should()
                .BeEquivalentTo(expected);
        }
    }
}