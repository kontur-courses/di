using FluentAssertions;
using NUnit.Framework;
using TagsCloudForm.WordFilters;
using TagsCloudForm.CircularCloudLayouterSettings;

namespace TagsCloudTests.FiltersTests
{
    [TestFixture]
    public class SpellCheckerTests
    {
        [Test]
        public void SpellChecker_FilterTest_ShouldFilterNotWords()
        {
            var checker = new SpellCheckerFilter();
            var words = new string[] {"www", "hello", "asd"};

            var filtered = checker.Filter(words, LanguageEnum.English);

            filtered.Should().BeEquivalentTo("hello");
        }

        [Test]
        public void SpellChecker_FilterTest_ShouldFilterNotWordsUpperCase()
        {
            var checker = new SpellCheckerFilter();
            var words = new string[] { "WWW", "Hello", "aSd" };

            var filtered = checker.Filter(words, LanguageEnum.English);

            filtered.Should().BeEquivalentTo("Hello");
        }
    }
}
