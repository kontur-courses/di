namespace TagCloudShould.Infrastructure
{
    [TestFixture]
    public class FrequencyTagsShould
    {
        [Test]
        public void CalculateCount_WhenDifferentTags()
        {
            var frequencyTags = new FrequencyTags().GetDictionaryWithTags(new[] { "1", "2", "3", "4" });
            frequencyTags.Keys.Count.Should().Be(4);
        }

        [Test]
        public void CalculateValueForEveryTag_WhenDifferentTags()
        {
            var frequencyTags = new FrequencyTags().GetDictionaryWithTags(new[] { "1", "2", "3", "4" });
            foreach (var pair in frequencyTags)
                pair.Value.Should().Be(1);
        }

        [Test]
        public void CalculateValue_WhenTagRepeat()
        {
            var frequencyTags = new FrequencyTags().GetDictionaryWithTags(new[] { "1", "2", "2", "3" });
            frequencyTags["2"].Should().Be(2);
        }

        [Test]
        public void ThrowException_WhenFrequencyTagsFieldNull()
        {
            Action action = () => new FrequencyTags().GetDictionaryWithTags(null);
            action.Should().Throw<ArgumentNullException>();
        }

    }
}
