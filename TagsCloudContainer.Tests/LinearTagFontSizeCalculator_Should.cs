using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TagFontSizeCalculator;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class LinearTagFontSizeCalculator_Should
    {
        private ITagFontSizeCalculatorSettings settings;
        private LinearTagFontSizeCalculator fontSizeCalculator;

        [SetUp]
        public void SetUp()
        {
            settings = A.Fake<ITagFontSizeCalculatorSettings>();
        }

        [TestCase(54, 85, 22, 3)]
        [TestCase(60, 98, 37, 2)]
        [TestCase(50, 66, 97, 12)]
        public void Calculate_Correctly(int count, int maxCount, int maxFontSize, int minFontSize)
        {
            A.CallTo(() => settings.MaxFontSize)
                .Returns(maxFontSize);
            A.CallTo(() => settings.MinFontSize)
                .Returns(minFontSize);
            fontSizeCalculator = new LinearTagFontSizeCalculator(settings);
            var expected = (count / (float) (maxCount == 0 ? 1 : maxCount)) * (maxFontSize - minFontSize) + minFontSize;

            fontSizeCalculator.Calculate(count, maxCount)
                .Should()
                .Be(expected);
        }
    }
}