using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordFilter;

namespace TagsCloudContainerTests.WordFilterTests
{
    [TestFixture]
    public class LengthWordFilterTests
    {
        [Test]
        public void LengthFilter_ShouldSkipShortWords()
        {
            var filterSettings = new FilterSettings(lengthForBoringWord: 2);
            var text = new[] {"hi", "verylong", "medium"};
            var filter = new LengthWordFilter(filterSettings);
            var expectedResult = new[] {"verylong", "medium"};

            var result = text.Where(word => filter.Validate(word));

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}