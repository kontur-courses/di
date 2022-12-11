using TagCloudContainer.FrequencyWords;

namespace TagCloudShould.Infrastructure
{
    [TestFixture]
    public class FrequencyTagsShould
    {
        [Test]
        public void ThrowException_WhenTextIsNull()
        {
            Action act = () => { new FrequencyTags().GetWordsFrequency(null); };
            act.Should().Throw<ArgumentNullException>();
        }
        [Test]
        public void ReturnTags_WhenHaveManyTimesOneTag()
        {
            var words = new List<string>()
            {
                "word", "word", "word", "word", "word", "word", "word", "word", "word", "word", "word", "word", "word"
            };
            var wordsFrequency = new FrequencyTags().GetWordsFrequency(words);
            wordsFrequency.Count().Should().Be(1);
            wordsFrequency.FirstOrDefault().Count.Should().Be(13);
        }
        [Test]
        public void ReturnTags_WhenHaveDifferentTags()
        {
            var words = new List<string>()
            {
                "word", "words", "word", "words", "word", "words", "word", "word", "words", "words", "word", "word", "word"
            };
            var wordsFrequency = new FrequencyTags().GetWordsFrequency(words);
            wordsFrequency.Count().Should().Be(2);

        }
        [Test]
        public void ReturnTags_WhenHaveBiggerTagFirst()
        {
            var words = new List<string>()
            {
                "word", "words", "word", "words", "word", "words", "word", "word", "words", "words", "word", "word", "word"
            };
            var wordsFrequency = new FrequencyTags().GetWordsFrequency(words);
            wordsFrequency.First().Word.Should().Be("word");
            wordsFrequency.FirstOrDefault().Count.Should().BeGreaterOrEqualTo(wordsFrequency.LastOrDefault().Count);
        }
        [Test]
        public void ReturnTags_WhenHaveManyTagsOneTimes()
        {
            var words = new List<string>()
            {
                "word", "words", "wor2d", "word3s", "word4", "w5ords", "wo1rd", "1word", "wo2rds", "word33s", "wor44d", "wor35d", "wo53rd"
            };
            var wordsFrequency = new FrequencyTags().GetWordsFrequency(words);
            wordsFrequency.Count().Should().Be(13);
            foreach (var frequency in wordsFrequency)
                frequency.Count.Should().Be(1);
        }
    }
}
