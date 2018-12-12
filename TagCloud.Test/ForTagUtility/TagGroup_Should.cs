using System;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Utility.Models.Tag;

namespace TagCloud.Tests.ForTagUtility
{
    [TestFixture]
    public class TagGroup_Should
    {
        [TestCase(0,TestName = "When font size is zero")]
        [TestCase(-1, TestName = "When font size is negative")]
        public void ConstructorThrowArgumentException(int fontSize)
        {
            Action constructor = () => new TagGroup(fontSize, new FrequencyGroup());

            constructor.Should().Throw<ArgumentException>();
        }
    }
}