using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class WordsFilterTests
    {
        private List<string> boringWords = new();
        private WordsFilter sut;

        [SetUp]
        public void Setup()
        {
            sut = new WordsFilter(new MyConfiguration
            {
                ExcludedParticals = "SPRO, PR, PART, CONJ"
            });
        }

        [TearDown]
        public void CleanUp()
        {
            boringWords = new();
        }

        [Test]
        public void FilterWords_AddNounTaggedWord_ShouldKeepWordInListAndRemoveTaggedInfo()
        {
            var taggedWords = new List<string> { "печь{печь=S,жен,неод=(вин,ед|им,ед)|печь=V,несов,пе=инф}" };
            var expectedResult = new List<string> { "печь" };

            var result = sut.FilterWords(taggedWords, boringWords);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void FilterWords_AddNounTaggedWord_ShouldLowerCaseIt()
        {
            var taggedWords = new List<string> { "Печь{печь=S,жен,неод=(вин,ед|им,ед)|печь=V,несов,пе=инф}" };
            var expectedResult = new List<string> { "печь" };

            var result = sut.FilterWords(taggedWords, boringWords);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void FilterWords_AddPRTaggedWord_ShouldRemoveItFromResult()
        {
            var taggedWords = new List<string> { "около{около=PR=|около=ADV=}" };
            var expectedResult = new List<string>();

            var result = sut.FilterWords(taggedWords, boringWords);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void FilterWords_AddTaggedPRInAnyPostitionWord_ShouldRemoveItFromResult()
        {
            var taggedWords = new List<string> { "около{около=ADV=|около=PR=}" };
            var expectedResult = new List<string>();

            var result = sut.FilterWords(taggedWords, boringWords);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void FilterWords_WithoutRestrictions_ShouldKeepAnyWord()
        {
            var filter = new WordsFilter(new MyConfiguration());
            var taggedWords = new List<string>
            {
                "около{около=ADV=|около=PR=}",
                "печь{печь=S,жен,неод=(вин,ед|им,ед)|печь=V,несов,пе=инф}"
            };
            var expectedResult = new List<string>
            {
                "около",
                "печь"
            };

            var result = filter.FilterWords(taggedWords, boringWords);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void FilterWords_AddBoringWords_ShouldExcludeIt()
        {
            var taggedWords = new List<string> { "печь{печь=S,жен,неод=(вин,ед|им,ед)|печь=V,несов,пе=инф}" };
            boringWords.Add("печь");
            var expectedResult = new List<string>();

            var result = sut.FilterWords(taggedWords, boringWords);

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}