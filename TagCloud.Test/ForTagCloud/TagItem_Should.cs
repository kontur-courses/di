using System;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Models;

namespace TagCloud.Tests.ForTagCloud
{
    [TestFixture]
    class TagItem_Should
    {
        [TestCase(0,0,TestName = "Then font size is zero")]
        [TestCase(-1, -1, TestName = "Then font size is negative")]
        public void ConstructorThrowsArgumentException(int width,int height)
        {
            Action constructor = () => new TagItem(null, 0);

            constructor.Should().Throw<ArgumentException>();
        }
    }
}