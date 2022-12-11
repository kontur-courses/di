using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class WordSizeCalculatorTests
    {
        private CustomOptions options;
        private WordSizeCalculator sut = new();

        [SetUp]
        public void SetUpOptions()
        {
            options = new CustomOptions { MaxTagSize = 30, MinTagSize = 10 };
        }

        [Test]
        public void CalculateSize_GiveWordWithOneRepitition_ShouldSizeItAsMinSize()
        {
            var input = new Dictionary<string, int> { { "Test", 1 } };

            var result = sut.CalculateSize(input, options);

            result["Test"].Size.Should().Be(10);
        }

        [Test]
        public void CalculateSize_GiveWordsWithOneAndTwoRepitition_ShouldSizeWhemAsMinAndMaxSize()
        {
            var input = new Dictionary<string, int>
            {
                { "Test1", 2 },
                { "Test2", 1 }
            };

            var result = sut.CalculateSize(input, options);

            result["Test1"].Size.Should().Be(30);
            result["Test2"].Size.Should().Be(10);
        }

        [Test]
        public void CalculateSize_GiveWordsWithMediumRepitition_ShouldSizeItAsMedium()
        {
            var input = new Dictionary<string, int>
            {
                { "Test1", 4 },
                { "Test2", 2 },
                { "Test3", 1 }
            };

            var result = sut.CalculateSize(input, options);

            result["Test2"].Size.Should().Be(20);
        }
    }
}