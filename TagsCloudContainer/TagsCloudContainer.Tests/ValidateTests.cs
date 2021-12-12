using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Settings;

namespace TagsCloud.Tests
{
    public class ValidateTests
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void Positive_ThrowsException_WhenValueNegative(int value) =>
            Assert.Throws<ApplicationException>(() => Validate.Positive("", value));

        [Test]
        public void Positive_ReturnValue_WhenValuePositive()
        {
            Validate.Positive("", 1)
                .Should().Be(1);
        }
    }
}