using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using TagCloud;

namespace TagCloudUnitTests
{
    internal class ConvertersExecutorTests
    {
        private ConvertersExecutor convertersExecutor;

        [SetUp]
        public void Setup()
        {
            convertersExecutor = new ConvertersExecutor();
        }

        [Test]
        public void Conver_ReturnsLowerCaseWords_WhenToLowerConverterRegistred()
        {
            convertersExecutor.RegisterConverter(new ToLowerConverter());

            var inputWords = new List<string>() { "OnE", "tWo", "thrEE", "FOUR", "FiVE", "six" };

            var expectedWords =  new List<string>() { "one", "two", "three", "four", "five", "six" };

            var actualWords = convertersExecutor.Convert(inputWords);

            actualWords.Should().BeEquivalentTo(expectedWords);
        }
    }
}
