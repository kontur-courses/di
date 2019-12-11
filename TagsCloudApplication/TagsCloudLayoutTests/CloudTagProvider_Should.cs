using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using TagsCloudVisualization;
using TextConfiguration;
using System.Linq;
using System.Drawing;
using System.Drawing.Text;

namespace TagsCloudApplicationTests
{
    [TestFixture]
    public class CloudTagProvider_Should
    {
        private class MockWordsProvider : IWordsProvider
        {
            private readonly List<string> words;

            public MockWordsProvider(IReadOnlyCollection<string> words)
            {
                this.words = words.ToList();
            }

            public List<string> ReadWordsFromFile(string filePath)
            {
                return words;
            }
        }

        private CloudTagProvider GetConstantCloudTagProvider(CloudTagProperties properties,
                                                             IReadOnlyCollection<string> words)
        {
            return new CloudTagProvider(properties, new MockWordsProvider(words));
        }

        private CloudTagProvider GetDefaultConstantCloudTagProvider(IReadOnlyCollection<string> words)
        {
            var properties = new CloudTagProperties(new FontFamily(GenericFontFamilies.SansSerif), 1);
            return GetConstantCloudTagProvider(properties, words);
        }

        [Test]
        public void ProvideProperly_OnEmptyWordCollection()
        {
            var cloudTagProvider = GetDefaultConstantCloudTagProvider(new string[] { });

            var tags = cloudTagProvider.ReadCloudTags(null);

            tags.Should().BeEquivalentTo(new CloudTag[] { });
        }

        [TestCase("one", "one", "one")]
        [TestCase("first", "second", "third")]
        public void ProvideTags_WhichContainOnlyProvidedWords(params string[] words)
        {
            var cloudTagProvider = GetDefaultConstantCloudTagProvider(words);

            var tags = cloudTagProvider.ReadCloudTags(null);

            tags.Select(tag => tag.Word).Should().BeSubsetOf(words);
        }

        [TestCase("one", "one", "one")]
        [TestCase("first", "second", "second", "first")]
        public void ProvideTags_WithNonEqualWords(params string[] words)
        {
            var cloudTagProvider = GetDefaultConstantCloudTagProvider(words);

            var tags = cloudTagProvider.ReadCloudTags(null);

            tags.Select(tag => tag.Word).Should().OnlyHaveUniqueItems();
        }

        [TestCase(GenericFontFamilies.SansSerif)]
        [TestCase(GenericFontFamilies.Monospace)]
        public void SetFontFamily_IndicatedInProperties(GenericFontFamilies fontFamilyType)
        {
            var words = Enumerable.Range(1, 1000).Select(numb => numb.ToString()).ToArray();
            var fontFamily = new FontFamily(fontFamilyType);
            var properties = new CloudTagProperties(fontFamily, 1);
            var cloudTagProvider = GetConstantCloudTagProvider(properties, words);

            var tags = cloudTagProvider.ReadCloudTags(null);

            tags.Select(tag => tag.Font.FontFamily).Distinct().Should().BeEquivalentTo(fontFamily);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void SetFontSize_GreaterOrEqualThanMin(int minFontSize)
        {
            var words = Enumerable.Range(1, 1000).Select(numb => numb.ToString()).ToArray();
            var properties = new CloudTagProperties(new FontFamily(GenericFontFamilies.SansSerif), minFontSize);
            var cloudTagProvider = GetConstantCloudTagProvider(properties, words);

            var tags = cloudTagProvider.ReadCloudTags(null);

            tags.Select(tag => tag.Font.Size).Min().Should().BeGreaterOrEqualTo(minFontSize);
        }

        [Test]
        public void SetFontSize_DependingOnAmountOfEntries()
        {
            var words = new string[] { "one", "three", "three", "three", "two", "two" };
            var cloudTagProvider = GetDefaultConstantCloudTagProvider(words);
            var expectedWordOrder = new string[] { "one", "two", "three" };

            var tags = cloudTagProvider.ReadCloudTags(null);

            tags.OrderBy(tag => tag.Font.Size).Select(tag => tag.Word).Should().BeEquivalentTo(expectedWordOrder);
        }
    }
}
