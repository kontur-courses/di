using System;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.TextHandlers.Converters;

namespace TagCloud.Tests
{
    [TestFixture]
    public class WordConverterTests
    {
        [Test]
        public void Convert_ShouldConvertWord()
        {
            var sut = new ConvertersPool(Array.Empty<IConverter>())
                .Using(s => s.ToLower())
                .Using(s => s.Substring(1));

            var converted = sut.Convert("AaAaaaA");

            converted.Should().BeEquivalentTo("aaaaaa");
        }
    }
}