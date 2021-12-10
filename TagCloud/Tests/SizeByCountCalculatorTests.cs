using FluentAssertions;
using NUnit.Framework;
using TagCloud.Templates;

namespace TagCloud.Tests
{
    [TestFixture]
    public class SizeByCountCalculatorTests
    {
        [Test]
        public void GetFontSizes_ShouldReturnRightSizes()
        {
            var words = new[] { "1", "2", "2", "3", "3", "3" };
            var sut = new FontSizeByCountCalculator(1, 10);

            var wordToSize = sut.GetFontSizes(words);

            wordToSize.Values.Should().BeEquivalentTo(new[] { 10, 1, 5.5f });
        }
    }
}