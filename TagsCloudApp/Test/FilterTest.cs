using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudApp.WordFiltering;

namespace TagsCloudApp.Test
{
    [TestFixture]
    public class FilterTest
    {
        [Test]
        public void FilterWords_ShouldFilter_UnusedPartsOfSpeech()
        {
            var words = new List<string> { "человек", "в", "пути", "нашел", "свой", "дом" };
            var filter = new Filter();
            var remainWords = filter.FilterWords(words);
            remainWords.Should().NotContain("в");
            remainWords.Should().NotContain("свой");
            remainWords.Should().Contain("человек");

        }
    }
}
