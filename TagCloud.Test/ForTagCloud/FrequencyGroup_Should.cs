using System;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Utility.Models.Tag;

namespace TagCloud.Tests.ForTagCloud
{
    [TestFixture]
    public class FrequencyGroup_Should
    {
        [TestCase(-1, 0, TestName = "When min is less than zero")]
        [TestCase(2, 0, TestName = "When min is bigger then 1")]
        [TestCase(0, -1, TestName = "When max is less than zero")]
        [TestCase(0, 2, TestName = "When max is bigger then 1")]
        [TestCase(0.7, 0.5, TestName = "When min is bigger then max")]
        public void ConstructorThrowArgumentException(double min, double max)
        {
            Action consrtuctor = () => new FrequencyGroup(min, max);

            consrtuctor.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 1, 0, 1, true, TestName = "True when group are intersected")]
        [TestCase(0, 0.5, 0.5, 1, false, TestName = "False when group aren't intersected")]
        public void MethodIntersectWith_Return(double minFirst, double maxFirst, double minSecond, double maxSecond, bool result)
        {
            var first = new FrequencyGroup(minFirst, maxFirst);
            var second = new FrequencyGroup(minSecond, maxSecond);

            first.IntersectWith(second).Should().Be(result);
        }


        [TestCase(0.5, true, TestName = "True when value is max freq coef")]
        [TestCase(0, true, TestName = "True when value is min freq coef")]
        [TestCase(0.5, true, TestName = "True when value is between min and max")]
        [TestCase(1, false, TestName = "False when value isn't between min and max")]
        public void MethodContains_Return(double value, bool isContains)
        {
            var group = new FrequencyGroup(0, 0.5);

            var result = group.Contains(value);

            result.Should().Be(isContains);
        }
    }
}