using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Models;

namespace TagCloud.Tests.ForTagCloud
{
    [TestFixture]
    class CloudItem_Should
    {
        [TestCase(0, 0, TestName = "Then size is zero")]
        [TestCase(-1, -1, TestName = "Then size is negative")]
        public void ConstructorThrowsArgumentException(int width, int height)
        {
            Action constructor = () => new CloudItem(null, new Rectangle(0, 0, width, height), new Font("arial", 15));

            constructor.Should().Throw<ArgumentException>();
        }
    }
}