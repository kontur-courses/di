using System;
using NUnit.Framework;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Tests
{
    public class FontSizeRangeTests
    {
        [TestCase(1, 0, TestName = "Min not positive")]
        [TestCase(0, 1, TestName = "Max not positive")]
        public void Constructor_ThrowsException(int max, int min) =>
            Assert.Throws<ArgumentException>(() => new FontSizeRange(max, min));
    }
}