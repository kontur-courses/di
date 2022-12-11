using TagCloudContainer.FrequencyWords;
using TagCloudContainer.TagsWithFont;

namespace TagCloudShould.Infrastructure
{
    [TestFixture]
    public class FontSizerShould
    {
        [Test]
        public void GetSize_WhenHaveOneTag()
        {
            var fontSizer = new FontSizer();
            var words = new List<string>()
            {
                "word", "word", "word", "word", "word", "word", "word", "word", "word", "word", "word", "word", "word"
            };
            var wordsFrequency = new FrequencyTags().GetWordsFrequency(words);
            var fontTags = fontSizer.GetTagsWithSize(wordsFrequency,
                new FontSettings() { Font = new FontFamily("Times"), MaxFont = 150, MinFont = 50 });
            fontTags.First().SizeFont.Should().Be(50);
        }
        [Test]
        public void GetSize_WhenHaveTwoTagsNotEqualCount()
        {
            var fontSizer = new FontSizer();
            var words = new List<string>()
            {
                "words", "words", "words", "words", "words", "word", "word", "word", "word", "word", "word", "word", "word"
            };
            var wordsFrequency = new FrequencyTags().GetWordsFrequency(words);
            var fontTags = fontSizer.GetTagsWithSize(wordsFrequency,
                new FontSettings() { Font = new FontFamily("Times"), MaxFont = 150, MinFont = 50 });
            fontTags.First().SizeFont.Should().Be(150);
            fontTags.Last().SizeFont.Should().Be(50);
        }
        [Test]
        public void GetSize_WhenHaveManyTagsNotEqualCount()
        {
            var fontSizer = new FontSizer();
            var words = new List<string>()
            {
                "words", "words", "words", "wordes", "words", "word", "words1", "words1", "words1", "word", "word", "word", "word"
            };
            var wordsFrequency = new FrequencyTags().GetWordsFrequency(words);
            var fontTags = fontSizer.GetTagsWithSize(wordsFrequency,
                new FontSettings() { Font = new FontFamily("Times"), MaxFont = 150, MinFont = 50 }).ToList();
            fontTags.First().SizeFont.Should().Be(150);
            fontTags.Last().SizeFont.Should().Be(50);
            fontTags[1].SizeFont.Should().Be(130);
            fontTags[2].SizeFont.Should().Be(110);
        }
    }
}
