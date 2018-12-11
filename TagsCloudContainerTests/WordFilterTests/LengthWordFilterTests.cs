using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordFilter;

namespace TagsCloudContainerTests.WordFilterTests
{
    [TestFixture]
    public class LengthWordFilterTests
    {
        [Test]
        public void LengthFilter_ShouldSkipShortWords()
        {
            var text = new[] {"hi", "verylong", "medium"};
            var filter = new LengthWordFilter(2);
            var expectedResult = new[] {"verylong", "medium"};

            var result = text.Where(word => filter.Validate(word));

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}