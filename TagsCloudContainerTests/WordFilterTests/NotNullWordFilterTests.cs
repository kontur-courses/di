using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordFilter;

namespace TagsCloudContainerTests.WordFilterTests
{
    [TestFixture]
    public class NotNullWordFilterTests
    {
        [Test]
        public void NotNullFilter_ShouldSkipEmptyWords()
        {
            var text = new[] {"hi", "", ""};
            var filter = new NotNullWordFilter();
            var expectedResult = new[] {"hi"};

            var result = text.Where(word => filter.Validate(word));

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void NotNullFilter_ShouldSkipNullWords()
        {
            var text = new[] {"hi", null, "medium"};
            var filter = new NotNullWordFilter();
            var expectedResult = new[] {"hi", "medium"};

            var result = text.Where(word => filter.Validate(word));

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}