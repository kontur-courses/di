using FluentAssertions;
using NUnit.Framework;
using TagCloud.TextHandlers.Filters;

namespace TagCloud.Tests
{
    [TestFixture]
    public class TextFilterTests
    {
        [Test]
        public void Filter_ShouldFilterWords()
        {
            var words = new[] { "12345", "123", "12", "1", "3334" };
            var expected = new[] { "12", "1" };
            var sut = new TextFilter().Using(w => w.Length < 3);

            var filtered = sut.Filter(words);

            filtered.Should().BeEquivalentTo(expected);
        }
    }
}