using FluentAssertions;
using NUnit.Framework;
using TagsCloudForm.CircularCloudLayouter;

namespace TagsCloudForm.Tests
{
    [TestFixture]
    public class SpellCheckerTests
    {
        [Test]
        public void FilterTest()
        {
            var checker = new SpellCheckerFilter();
            var words = new string[] {"www", "hello", "asd"};
            checker.Filter(words, LanguageEnum.English).Should().BeEquivalentTo(new string[] { "hello" });
        }
    }
}
