using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Processing.Converting;

namespace TagsCloudContainerTests.Processing
{
    public class WordConverterShould
    {
        [Test]
        public void ConvertWordsToLower()
        {
            new DefaultConverter().Convert(new[] {"Hello", "ADAM", "22", "ItaliC"}).Should()
                .BeEquivalentTo("hello", "adam", "22", "italic");
        }
    }
}