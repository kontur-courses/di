using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using TagCloud.WordConverter;

namespace TagCloudUnitTests
{
    [TestFixture]
    internal class ConvertersExecutorTests
    {
        [Test]
        public void Convert_ReturnsLowerCaseWords_WhenToLowerConverterRegistred()
        {
            var convertersExecutor = new ConvertersExecutor(new IWordConverter[] { new ToLowerConverter()});

            var inputWords = new List<string>() { "OnE", "tWo", "thrEE", "FOUR", "FiVE", "six" };

            var expectedWords =  new List<string>() { "one", "two", "three", "four", "five", "six" };

            var actualWords = convertersExecutor.Convert(inputWords);

            actualWords.Should().BeEquivalentTo(expectedWords);
        }
    }
}
