using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using TagCloud.WordConverter;

namespace TagCloudUnitTests
{
    internal class ConvertersExecutorTests
    {
        private ConvertersExecutor convertersExecutor;

        [SetUp]
        public void Setup()
        {
            convertersExecutor = new ConvertersExecutor( new IWordConverter[] { });
        }

        [Test]
        public void Convert_ReturnsLowerCaseWords_WhenToLowerConverterRegistred()
        {
            convertersExecutor.RegisterConverter(new ToLowerConverter());

            var inputWords = new List<string>() { "OnE", "tWo", "thrEE", "FOUR", "FiVE", "six" };

            var expectedWords =  new List<string>() { "one", "two", "three", "four", "five", "six" };

            var actualWords = convertersExecutor.Convert(inputWords);

            actualWords.Should().BeEquivalentTo(expectedWords);
        }
    }
}
