using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordConverter;

namespace TagsCloudContainerTests.WordConverterTests
{
    [TestFixture]
    public class InitialFormWordConverterTests
    {
        [Test]
        public void InitialWordConverter_Should_ConvertToInitialFrom()
        {
            var text = new[] {"стула", "словари", "океаны"};
            var converter = new InitialFormWordConverter();
            var expectedResult = new[] {"стул", "словарь", "океан"};

            var result = text.Select(word => converter.Convert(word)).ToArray();

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}