using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using TagCloud.WordFilter;

namespace TagCloudUnitTests
{
    [TestFixture]
    internal class FiltersExecutorTests
    {
        private FiltersExecutor filtersExecutor;

        [SetUp]
        public void Setup()
        {
            filtersExecutor = new FiltersExecutor(new IWordFilter[] { });
        }

        [Test]
        public void Conver_ReturnsLowerCaseWords_WhenToLowerConverterRegistred()
        {
            filtersExecutor.RegisterFilter(new BoringWordFilter());

            var inputWords = new List<string>() { "one", "two", "three", "four", "five", "six" };

            var expectedWords = new List<string>() {"three", "four", "five"};

            var actualWords = filtersExecutor.Filter(inputWords);

            actualWords.Should().BeEquivalentTo(expectedWords);
        }
    }
}
