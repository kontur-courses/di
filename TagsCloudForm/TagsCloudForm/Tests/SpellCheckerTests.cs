using FluentAssertions;
using NUnit.Framework;
using TagsCloudForm.CircularCloudLayouter;
using TagsCloudForm.WordFilters;

namespace TagsCloudForm.Tests
{
    [TestFixture]
    public class SpellCheckerTests
    {
        [Test]
        public void SpellChecker_FilterTest_ShouldFilterNotWords()
        {
            var checker = new SpellCheckerFilter();
            var words = new string[] {"www", "hello", "asd"};

            checker.Filter(words, LanguageEnum.English).Should().BeEquivalentTo(new string[] { "hello" });
        }

        [Test]
        public void SpellChecker_FilterTest_ShouldFilterUpperCaseWords()
        {
            var checker = new SpellCheckerFilter();
            var words = new string[] { "WWW", "Hello", "aSd" };

            checker.Filter(words, LanguageEnum.English).Should().BeEquivalentTo(new string[] { "Hello" });
        }
    }
}
