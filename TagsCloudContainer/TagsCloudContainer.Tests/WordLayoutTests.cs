using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Tests
{
    public class WordLayoutTests
    {
        private WordLayout first;
        private WordLayout second;

        [SetUp]
        public void SetUp()
        {
            first = new WordLayout("a", new Point(1, 2), new Font(FontFamily.GenericMonospace, 10));
            second = new WordLayout("a", new Point(1, 2), new Font(FontFamily.GenericMonospace, 10));
        }


        [Test]
        public void GetHashCode_WithEqualObjects_Equal()
        {
            first.GetHashCode()
                .Should().Be(second.GetHashCode());
        }

        [Test]
        public void Equals_WithEqualObjects_True()
        {
            first.Should().Be(second);
        }

        [Test]
        public void Equals_WithOtherObject_False()
        {
            var other = new WordLayout("b", new Point(1, 1), new Font(FontFamily.GenericMonospace, 10));
            first.Should().NotBe(other);
        }

        [Test]
        public void Equals_WithNull_False()
        {
            first.Should().NotBe(null);
        }
    }
}