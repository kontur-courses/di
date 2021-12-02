using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Tests
{
    public class WordLayoutTests
    {
        [Test]
        public void GetHashCode_WithEqualObjects_Equal()
        {
            var first = new WordLayout("a", new Point(1, 2), new Font(FontFamily.GenericMonospace, 10));
            var second = new WordLayout("a", new Point(1, 2), new Font(FontFamily.GenericMonospace, 10));

            first.GetHashCode()
                .Should().Be(second.GetHashCode());
        }

        [Test]
        public void Equals_WithEqualObjects_True()
        {
            var first = new WordLayout("a", new Point(1, 2), new Font(FontFamily.GenericMonospace, 10));
            var second = new WordLayout("a", new Point(1, 2), new Font(FontFamily.GenericMonospace, 10));

            first.Should().Be(second);
        }

        [Test]
        public void Equals_WithOtherObject_False()
        {
            var first = new WordLayout("a", new Point(1, 2), new Font(FontFamily.GenericMonospace, 10));
            var second = new WordLayout("b", new Point(1, 2), new Font(FontFamily.GenericMonospace, 10));

            first.Should().NotBe(second);
        }

        [Test]
        public void Equals_WithNull_False()
        {
            var first = new WordLayout("a", new Point(1, 2), new Font(FontFamily.GenericMonospace, 10));

            first.Should().NotBe(null);
        }
    }
}